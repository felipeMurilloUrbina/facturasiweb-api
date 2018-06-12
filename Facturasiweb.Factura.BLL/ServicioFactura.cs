using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facturasiweb.Factura.DAO;
using Facturasiweb.Factura.LogDLL;
using Facturasiweb.Factura.Modelo;
using Facturasiweb.Factura.Modelo.DTO;
using System.Data.Entity;
using Facturasiweb.Factura.BLL.Modelos;
using Facturasiweb.Factura.BLL.Servicios;
using System.Web;
using Facturasiweb.Factura.TimbradoDLL;
using System.Xml;
using Facturasiweb.Factura.Reportes.Servicios;
using System.IO;
using System.Net.Http;

namespace Facturasiweb.Factura.BLL
{
    public class ServicioFactura : ServicioBase
    {
        public ServicioFactura(Logger logger, Context contexto) : base(logger, contexto)
        {
        }
        public ICollection<Modelo.Factura> Get(UsuarioDto usuario, int sucursalId)
        {
            ICollection<Modelo.Factura> _facturas;
            this._contexto.disabled();
            switch (usuario.Tipo.ToLower())
            {
                case "cliente-cliente":
                    _facturas = this._contexto.Facturas.Include(f => f.Cliente).Include(f => f.Detalles).Where(f => f.ClienteId == usuario.ClienteId).OrderByDescending(f => f.Fecha).ToList();
                    break;
                default:
                    _facturas = this._contexto.Facturas.Include(f => f.Cliente).Include(f => f.Detalles).Where(f => f.SucursalId == sucursalId).OrderByDescending(f => f.Fecha).ToList();
                    break;
            }
            this._contexto.enabled();
            return _facturas;
        }
        public ICollection<Modelo.Factura> Get(int clienteId, int sucursalId)
        {
            return this._contexto.Facturas.Include(f => f.Cliente).Include(f => f.Detalles).Where(f => f.ClienteId == clienteId && f.SucursalId == sucursalId).ToList();
        }
        public Modelo.Factura GetId(int facturaId)
        {
            return this._contexto.Facturas.
                 Include(f => f.Detalles).
                 Include("Detalles.CatSatProducto").
                 Include("Detalles.CatSatUnidad").
                 Include("Regimen").
                 Include(f => f.MetodoPago).
                 Include(f => f.FormaPago).
                 Include(f => f.UsoCFDI).
                 Include(f => f.Cliente).
                 Include(f => f.Sucursal).
                 Include(f=> f.Complementos).
                 Include(f => f.Formato).Where(f => f.Id == facturaId).FirstOrDefault();
        }
        public FacturaNueva GetNueva(int sucursalId)
        {
            var _sucursal = this._contexto.Sucursales.Include(s => s.Regimenes).Include("Regimenes.Regimen").Where(f => f.Id == sucursalId).FirstOrDefault();
            var _facturas = this._contexto.Facturas.Where(f => f.SucursalId == sucursalId && f.Serie.Equals(_sucursal.Serie)).ToList();
            var _folio = _facturas.Count()==0 || _facturas==null ? 0 : _facturas.Max(f=> f.Folio);
            _folio++;
            return new FacturaNueva()
            {
                Folio = _folio,
                Serie = _sucursal.Serie,
                Regimenes = _sucursal.Regimenes.ToList()
            };
        }
        public Boolean Post(ref string error, Modelo.Factura factura, UsuarioDto usuario, HttpContext context)
        {
            var _servicioRelacion = new ServicioRelaciones(this._logger, this._contexto);
            _servicioRelacion.ObtenerRelacionFactura(ref factura);
            Ruta ruta = new Ruta(context, factura);
            if (!ValidarFactura(ref error, ref factura))
                return false;
            ServicioXml _servicioXml = new ServicioXml(this._logger);
            if (!_servicioXml.GeneraCFDI(ref error, factura, ruta.RutaGuardadoXML, ruta.RutaCertificado, ruta.RutaKey, ruta.RutaArchivosXSLT, ruta.RutaCarpetaFacturasClientes))
                return false;
            XmlDocument _xmlDocument = new XmlDocument();
            if (!_servicioXml.Timbrar(ref error, ref _xmlDocument, ruta.RutaGuardadoXML, ruta.RutaGuardadoXML))
                return false;
            Put(ref error, ref factura, _xmlDocument);
            try
            {
                factura.ImagenCbb = ruta.RutaImagenCBB;
                _servicioXml.GeneraCBB(ref error, factura.Sucursal.Rfc, factura.Cliente.Rfc, factura.FolioFiscal, factura.Total, ruta.RutaCBB);
                var correo = factura.Cliente.Correo;
                GenerarPDF(factura, ruta);
                ServicioCorreo.Enviar(factura.Sucursal.Asunto, factura.Sucursal.Mensaje, correo, ruta.RutaPDF, ruta.RutaGuardadoXML, this._logger);
                _servicioRelacion.SanitizarFactura(ref factura);
                Save(ref error, factura);
                return true;
            }
            catch (Exception e)
            {
                this._logger.EscribirError(e.ToString());
                error = "Ocurrio un error al guardar la factura";
                return false;

            }
        
        }
        public FileStream GetFile(ref string nombre, ref Modelo.Factura factura, int opcion, int facturaId, HttpContext contexto)
        {
            factura = GetId(facturaId);
            ServicioXml _servicio = new ServicioXml(this._logger);
            FileStream _archivo = null;
            string _error = string.Empty;
            Ruta _ruta = new Ruta(contexto, factura);
            new ServicioArchivo(_logger).CrearCarpeta(_ruta.RutaCarpetaFacturasClientes);
            switch (opcion)
            {
                case 1:
                    if (!File.Exists(_ruta.RutaGuardadoXML))
                    {
                        _servicio.RecuperarXML(factura.Sucursal.Rfc, factura.FolioFiscal, _ruta.RutaGuardadoXML);
                    }
                    _archivo = new FileStream(_ruta.RutaGuardadoXML, FileMode.Open, FileAccess.Read);
                    nombre = _ruta.NombreArchivoCompletoXml;
                    break;
                case 2:
                    if (!File.Exists(_ruta.RutaPDF))
                    {
                        _servicio.GeneraCBB(ref _error, factura.Sucursal.Rfc, factura.Cliente.Rfc, factura.FolioFiscal, factura.Total, _ruta.RutaCBB);
                        GenerarPDF(factura, _ruta);
                    }
                    nombre = _ruta.NombreArchivoCompletoPDF;
                    if (File.Exists(_ruta.RutaPDF))
                        _archivo = new FileStream(_ruta.RutaPDF, FileMode.Open, FileAccess.Read);
                    break;
            }
            return _archivo;
        }
        public Boolean Reenviar(ref string error, UsuarioDto usuario, int facturaId, HttpContext context)
        {
            Modelo.Factura _factura = new Modelo.Factura();
            ServicioXml _servicioXml = new ServicioXml(this._logger);
            string _nombre = "";
            GetFile(ref _nombre, ref _factura, 1, facturaId, context);
            GetFile(ref _nombre, ref _factura, 2, facturaId, context);
            Ruta _ruta = new Ruta(context, _factura);
            if (!File.Exists(_ruta.RutaGuardadoXML) || !File.Exists(_ruta.RutaPDF))
            {
                error = "No se recuperaron los archivos necesarios.";
                return false;
            }
            ServicioCorreo.Enviar(_factura.Sucursal.Asunto, _factura.Sucursal.Mensaje, _factura.Cliente.Correo, _ruta.RutaPDF, _ruta.RutaGuardadoXML, this._logger);

            return true;
        }
        public Boolean Cancelar(ref string error, int facturaId, UsuarioDto usuario, HttpContext contexto)
        {
            var _factura = GetId(facturaId);
            if (_factura == null)
            {
                error = "No existe la factura solicitada.";
                return false;
            }
            Ruta _ruta = new Ruta(contexto, _factura);
            if (!File.Exists(_ruta.RutaKey) && !File.Exists(_ruta.RutaCertificado))
            {
                error = ("No existen Certificado o Llave Privada del sat");
                return false;
            }
            var _servicioXml = new ServicioXml(this._logger);
            _servicioXml.Cancelar(ref error, ref _factura, _ruta.RutaCertificado, _ruta.RutaKey);
            Put(ref error, _factura);
            return true;
        }
        public void Put(ref string error, ref Modelo.Factura factura, XmlDocument xmlDocumento)
        {
            try
            {
                XmlNodeList nodoss = xmlDocumento.GetElementsByTagName("tfd:TimbreFiscalDigital");
                XmlAttributeCollection nodos = nodoss[0].Attributes;
                factura.FechaTimbrado = nodos["FechaTimbrado"].InnerText;
                factura.FolioFiscal = nodos["UUID"].InnerText;
                factura.NoCertificadoSat = nodos["NoCertificadoSAT"].InnerText;
                factura.SelloCfd = nodos["SelloCFD"].InnerText;
                factura.SelloSat = nodos["SelloSAT"].InnerText;
                factura.VersionTimbrado = nodos["Version"].InnerText;
                factura.Estatus = "Timbrada";
                factura.CantidadEnLetra = ServicioNumeroLetra.Convertir(factura.Total.ToString("F"), true);
            }
            catch (Exception e)
            {
                this._logger.EscribirError(e.ToString());
            }
        }
        public void Put(ref string error, Factura.Modelo.Factura factura)
        {
            try
            {
                this._contexto.Entry(factura).State = EntityState.Modified;
                this._contexto.SaveChanges();
            }
            catch (Exception e)
            {
                this._logger.EscribirError(e.ToString());
            }
        }
        public Boolean GenerarPDF(Modelo.Factura factura, Ruta ruta)
        {
            string respuesta = "";
            try
            {
                factura.ImagenCbbBytes = ruta.GetImageByte(ruta.RutaCBB);
                factura.Sucursal.LogoBytes = ruta.GetImageByte(HttpContext.Current.Server.MapPath(String.Format(Ruta.RutaImagenes, factura.Sucursal.Id, factura.Sucursal.Rfc, factura.Sucursal.Logo)));
                var facturaReporte = ServicioReporte.GenerarPDF(ref respuesta, factura, this._logger);
                if (facturaReporte != null)
                    facturaReporte.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, ruta.RutaPDF);
                return true;
            }
            catch (Exception e)
            {
                this._logger.EscribirError(e.ToString());
                return false;
            }
        }
        public void Save(ref string error, Modelo.Factura factura)
        {
            this._contexto.Facturas.Add(factura);
            try
            {
                this._contexto.SaveChanges();
            }
            catch (Exception e)
            {
                this._logger.EscribirError(e.ToString());
            }
        }
        public static bool ValidarFactura(ref string error, ref Modelo.Factura factura)
        {
            factura.Activo = true;
            factura.Tipo = "I";
            factura.Estatus = "A";
            factura.Fecha = DateTime.Now;
            if (factura.ClienteId == 0)
                error += "Favor de seleccionar un cliente.";
            if (factura.MetodopagoId == 0)
                error += "Favor de seleccionar un Metodo de pago.";
            if (factura.MetodopagoId == 0)
                error += "Favor de seleccionar una forma de pago.";
            if (factura.Detalles.Count() == 0)
                error += "La Factura necesita de un detalle.";
            if (error.Length > 0)
                return false;
            else
                return true;
        }
    }
}
