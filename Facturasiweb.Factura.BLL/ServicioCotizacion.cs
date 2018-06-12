using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Facturasiweb.Factura.BLL.Modelos;
using Facturasiweb.Factura.BLL.Servicios;
using Facturasiweb.Factura.DAO;
using Facturasiweb.Factura.LogDLL;
using Facturasiweb.Factura.Modelo;
using Facturasiweb.Factura.Modelo.DTO;
using Facturasiweb.Factura.Reportes.Servicios;

namespace Facturasiweb.Factura.BLL
{
    public class ServicioCotizacion : ServicioBase
    {
        public ServicioCotizacion(Logger logger, Context contexto) : base(logger, contexto)
        {
        }

        public ICollection<Cotizacion> Get(int sucursalId)
        {
            try
            {
                return this._contexto.Cotizaciones.
                    Include(c => c.Cliente).
                    Include(c => c.Sucursal).
                    Include(c => c.Usuario).
                    Include(c => c.Archivos).
                    Include(c => c.Estatus).
                    Include(c => c.Regimen).
                    Include(c => c.Detalles).
                    Include(c => c.FormaPago).
                    Include("Detalles.CatSatProducto").
                    Include("Detalles.CatSatUnidad").
                    Include(c => c.MetodoPago).
                    Where(c => c.SucursalId == sucursalId).ToList();

            }
            catch (Exception e )
            {
                this._logger.EscribirError(e.ToString());
                return null;   
            }
        }
        public Cotizacion GetId(int id)
        {
            return this._contexto.Cotizaciones.
                Include(c => c.Cliente).
                Include(c => c.Sucursal).
                Include(c => c.Usuario).
                Include(c => c.Regimen).
                Include(c => c.Estatus).
                Include(c => c.Formato).
                Include(c => c.Archivos).
                Include(c => c.Detalles).
                Include("Detalles.CatSatProducto").
                Include("Detalles.CatSatUnidad").
                Include(c => c.FormaPago).
                Include(c => c.MetodoPago).
                Where(c => c.Id == id).FirstOrDefault();
        }
        public CotizacionNueva GetNueva(int sucursalId)
        {
            var _facturas = this._contexto.Facturas.Where(f => f.SucursalId == sucursalId && f.Serie.Equals("C")).ToList();
            int? _folio = _facturas.Count == 0 ? 0 : _facturas.Max(f => f.Folio);
            _folio = _folio ?? 0;
            _folio++;
            return new CotizacionNueva()
            {
                Folio = _folio,
                Serie = "C"
            };

        }
        public Boolean Post(ref string error, Cotizacion cotizacion)
        {
            cotizacion.Fecha = DateTime.Now;
            var _servicioRelacion = new ServicioRelaciones(this._logger, this._contexto);
            cotizacion.CantidadEnLetra = ServicioNumeroLetra.Convertir(cotizacion.Total.ToString("F"), true);
            _servicioRelacion.SanitizarCotizacion(ref cotizacion);
            this._contexto.Cotizaciones.Add(cotizacion);
            try
            {
                this._contexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                this._logger.EscribirError(e.ToString());
                error = "Ocurrio un error al guardar cotización";
                return false;
            }
        }
        public Boolean Put(ref string error, Cotizacion cotizacion)
        {
            cotizacion.CantidadEnLetra = ServicioNumeroLetra.Convertir(cotizacion.Total.ToString("F"), true);
            this._contexto.Entry(cotizacion).State = EntityState.Modified;
            try
            {
                this._contexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                this._logger.EscribirError(e.ToString());
                error = "Ocurrio un error al guardar cotización";
                return false;
            }
        }

        public Boolean UploadFile(ref string error, HttpContext contexto, int cotizacionId, UsuarioDto usuario)
        {
            try
            {
                ServicioArchivo _servicioArchivo = new ServicioArchivo(this._logger);
                var _cotizacion = GetId(cotizacionId);
                Ruta _ruta = new Ruta(contexto, _cotizacion);
                var _archivo = contexto.Request.Files[0];
                _servicioArchivo.CrearCarpeta(_ruta.RutaCarpetaCotizacionesClientes);
                _archivo.SaveAs(Path.Combine(_ruta.RutaCarpetaCotizacionesArchivosClientes, _archivo.FileName));
                _cotizacion.Archivos.Add(new CotizacionTieneArchivo()
                {
                    CotizacionId = _cotizacion.Id,
                    NombreFisico = _archivo.FileName,
                    NombreHistorico = _archivo.FileName,
                    UsuarioId = usuario.Id
                });
                _contexto.Entry(_cotizacion).State = EntityState.Modified;
                _contexto.SaveChanges();
                return true;
            }
            catch (Exception e )
            {
                this._logger.EscribirError(e.ToString());
                error = "Ocurrio un error al subir archivo.";
                return false;
            }

        }
        public FileStream GetFile(ref string error, ref string nombre, ref Cotizacion cotizacion, int archivoId, HttpContext contexto)
        {
            cotizacion = GetId(cotizacion.Id);
            Ruta _ruta = new Ruta(contexto, cotizacion);
            var _archivo = cotizacion.Archivos.Where(ca => ca.Id == archivoId).FirstOrDefault();
            if(_archivo ==null)
            {
                error = "No existe archivo.";
                return null;
            }
            FileStream _archivoRespuesta = null;
            var _rutaArchivo = Path.Combine(_ruta.RutaCarpetaCotizacionesArchivosClientes, _archivo.NombreFisico);
            if(File.Exists(_rutaArchivo))
            {
                nombre = _archivo.NombreFisico; 
                _archivoRespuesta = new FileStream(_rutaArchivo, FileMode.Open, FileAccess.Read);
                return _archivoRespuesta;
            }
            else{
                error = "No existe archivo.";
                return null;
            }
        }
        public FileStream GetPDF(ref string error, ref string nombre, ref Cotizacion cotizacion, HttpContext contexto)
        {
            FileStream _archivo = null;
            cotizacion = GetId(cotizacion.Id);
            Ruta _ruta = new Ruta(contexto, cotizacion);
            GenerarPDF(ref error, cotizacion, _ruta);
            if (File.Exists(_ruta.RutaCotizacionPDF))
                _archivo = new FileStream(_ruta.RutaCotizacionPDF, FileMode.Open, FileAccess.Read);
            return _archivo;
        }
        public Boolean GenerarPDF(ref string error, Cotizacion cotizacion, Ruta ruta)
        {
            try
            {
                cotizacion.Sucursal.LogoBytes = ruta.GetImageByte(HttpContext.Current.Server.MapPath(String.Format(Ruta.RutaImagenes, cotizacion.Sucursal.Id, cotizacion.Sucursal.Rfc, cotizacion.Sucursal.Logo)));
                var cotizacionReporte = ServicioCotizacionReporte.GenerarPDF(ref error, cotizacion, this._logger);
                if (cotizacionReporte != null)
                    cotizacionReporte.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, ruta.RutaCotizacionPDF);
                return true;
            }
            catch (Exception e)
            {
                this._logger.EscribirError(e.ToString());
                return false;
            }
        }
    }
}
