using Facturasiweb.Factura.Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Facturasiweb.Factura.BLL.Servicios
{
    //public static class Ruta
    //{
    //    public static string RutaError = "~/App_Data/errores";
    //    public static string RutaCarpetaCertificados = "~/App_Data/archivos/{0}-{1}/certificados";
    //    public static string RutaCertificados = "~/App_Data/archivos/{0}-{1}/certificados/{2}";
    //    public static string RutaError = "~/App_Data/errores";
    //    public static string RutaError = "~/App_Data/errores";
    //}
    public class Ruta
    {
        public static string RutaCarpetaCertificados = "~/App_Data/archivos/{1}-{2}/{0}";
        public static string RutaCertificados = "~/App_Data/archivos/{0}-{1}/{2}/{3}";
        public static string RutaCarpetaTemp = "~/App_Data/temp/{1}";
        public static string RutaCarpetaSucursal = "~/App_Data/archivos/{0}-{1}";
        public static string RutaTempArchivo = "~/App_Data/temp/{1}";
        public static string NombreCFDITemp = "CFDI33.xml";
        public static string RutaCarpetaFacturas = "~/App_Data/archivos/{0}-{1}/{2}";
        public static string RutaCotizacionesArchivos = "~/App_Data/archivos/{0}-{1}/{2}/{1}{3}-{4}/";
        public static string RutaCarpetaCotizaciones = "~/App_Data/archivos/{0}-{1}/cotizaciones";
        public static string RutaCarpetaError = "~/App_Data/archivos/{0}-{1}/errores";
        public static string RutaCarpetaComplementos = "~/App_Data/archivos/{0}-{1}/complementos";
        public static string RutaArchivos = "~/App_Data/archivos/{0}-{1}/{5}/{1}{2}-{3}.{4}";
        public static string RutaArchivosCotizacion = "~/App_Data/archivos/{0}-{1}/{5}/{1}{2}-{3}/{1}{2}-{3}.{4}";
        public static string RutaCarpetaImagenes = "~/App_Data/archivos/{0}-{1}/imagenes";
        public static string RutaImagenes = "~/App_Data/archivos/{0}-{1}/imagenes/{2}";
        public static string NombreArchivo = "{0}{1}-{2}.{3}";
        HttpContext _contexto = null;
        Modelo.Factura _factura = null;
        Cotizacion _cotizacion = null;
        public Ruta(HttpContext _contexto, Modelo.Factura factura)
        {
            this._contexto = _contexto;
            this._factura = factura;
        }
        public Ruta(HttpContext _contexto, Cotizacion cotizacion)
        {
            this._contexto = _contexto;
            this._cotizacion = cotizacion;
        }
        public Ruta(HttpContext _contexto)
        {
            this._contexto = _contexto;
        }
        public byte[] GetImageByte(string ruta)
        {
            return File.Exists(ruta) ? FileToByteArray(ruta) : null;
        }
        public byte[] FileToByteArray(string fileName)
        {
            byte[] buff = null;
            FileStream fs = new FileStream(fileName,
                                           FileMode.Open,
                                           FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            long numBytes = new FileInfo(fileName).Length;
            buff = br.ReadBytes((int)numBytes);
            fs.Close();
            return buff;
        }
        /// <summary>
        /// Retorna la ruta de la carpeta archivos  de la sucursal.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rfc"></param>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public string RutaCarpetaArchivosSucursal(string id, string rfc, string tipo)
        {

            return _contexto.Server.MapPath(string.Format(RutaCarpetaCertificados, tipo, id, rfc));
        }
        /// <summary>
        /// Retorna la ruta del archivo donde se guardara de la sucursal.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rfc"></param>
        /// <param name="tipo"></param>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public string RutaArchivosSucursal(string id, string rfc, string tipo, string nombre)
        {

            return _contexto.Server.MapPath(string.Format(RutaCertificados, id, rfc, tipo, nombre));
        }
        public string RutaCarpetaErrorUsuario
        {
            get
            {
                return _contexto.Server.MapPath(string.Format(RutaCarpetaError, _factura.Sucursal.Id, _factura.Sucursal.Rfc));
            }
        }
        public string RutaGuardadoXML
        {
            get
            {
                return _contexto.Server.MapPath(String.Format(RutaArchivos, _factura.SucursalId, _factura.Sucursal.Rfc, _factura.Serie, _factura.Folio, "xml", "facturas"));
            }
        }
        public string NombreArchivoCompletoXml
        {
            get
            {
                return String.Format(NombreArchivo, _factura.Sucursal.Rfc, _factura.Serie, _factura.Folio, "XML");
            }
        }
        public string NombreArchivoCompletoPDF
        {
            get
            {
                return String.Format(NombreArchivo, _factura.Sucursal.Rfc, _factura.Serie, _factura.Folio, "PDF");
            }
        }
        public string NombreArchivoCompletoCBB
        {
            get
            {
                return String.Format(NombreArchivo, _factura.Sucursal.Rfc, _factura.Serie, _factura.Folio, "BMP");
            }
        }
        public string RutaTemporalXML
        {
            get
            {
                return Path.Combine(_contexto.Server.MapPath(string.Format(RutaCarpetaFacturas, _factura.Sucursal.Id, _factura.Sucursal.Rfc, "facturas")), NombreCFDITemp); //_contexto.Server.MapPath(String.Format(Ruta.RutaArchivos, _factura.SucursalId, _factura.Sucursal.Rfc, _factura.Serie, _factura.Folio, "xml", "_facturas"));
            }
        }
        public string RutaCBB
        {
            get
            {
                return _contexto.Server.MapPath(String.Format(RutaArchivos, _factura.Sucursal.Id, _factura.Sucursal.Rfc, _factura.Serie, _factura.Folio, "bmp", "facturas"));
            }
        }
        public string RutaPDF
        {
            get
            {
                return _contexto.Server.MapPath(String.Format(RutaArchivos, _factura.Sucursal.Id, _factura.Sucursal.Rfc, _factura.Serie, _factura.Folio, "pdf", "facturas"));
            }
        }
        public string RutaCotizacionPDF
        {
            get
            {
                return _contexto.Server.MapPath(String.Format(RutaArchivosCotizacion, _factura.Sucursal.Id, _factura.Sucursal.Rfc, _factura.Serie, _factura.Folio, "pdf", "cotizaciones"));
            }
        }
        public string RutaArchivosXSLT
        {
            get
            {
                return _contexto.Server.MapPath("~/xslt/cadenaoriginal_3_3.xslt");
            }
        }
        public string RutaKey
        {
            get
            {
                return _contexto.Server.MapPath(string.Format(RutaCertificados, _factura.Sucursal.Id, _factura.Sucursal.Rfc, "certificados", _factura.Sucursal.RutaKey));
            }
        }
        /// <summary>
        /// Retorna la ruta del certificado para la sucursal.
        /// </summary>
        public string RutaCertificado
        {
            get
            {
                return _contexto.Server.MapPath(string.Format(RutaCertificados, _factura.Sucursal.Id, _factura.Sucursal.Rfc, "certificados", _factura.Sucursal.RutaCer));
            }
        }
        public string RutaCarpetaFacturasClientes
        {
            get
            {
                if (!Directory.Exists(_contexto.Server.MapPath(string.Format(RutaCarpetaFacturas, _factura.Sucursal.Id, _factura.Sucursal.Rfc, "facturas"))))
                {
                    Directory.CreateDirectory(_contexto.Server.MapPath(string.Format(RutaCarpetaFacturas, _factura.Sucursal.Id, _factura.Sucursal.Rfc, "facturas")));
                }
                return _contexto.Server.MapPath(string.Format(RutaCarpetaFacturas, _factura.Sucursal.Id, _factura.Sucursal.Rfc, "facturas"));
            }
        }
        public string RutaCarpetaCotizacionesClientes
        {
            get
            {
                if (!Directory.Exists(_contexto.Server.MapPath(string.Format(RutaCarpetaFacturas, _cotizacion.Sucursal.Id, _cotizacion.Sucursal.Rfc, "cotizaciones"))))
                {
                    Directory.CreateDirectory(_contexto.Server.MapPath(string.Format(RutaCarpetaFacturas, _cotizacion.Sucursal.Id, _cotizacion.Sucursal.Rfc, "cotizaciones")));
                }
                return _contexto.Server.MapPath(string.Format(RutaCarpetaFacturas, _cotizacion.Sucursal.Id, _cotizacion.Sucursal.Rfc, "cotizaciones"));
            }
        }
        public string RutaCarpetaCotizacionesArchivosClientes
        {
            get
            {
                var _ruta = _contexto.Server.MapPath(string.Format(RutaCotizacionesArchivos, _cotizacion.Sucursal.Id, _cotizacion.Sucursal.Rfc, "cotizaciones", _cotizacion.Serie, _cotizacion.Folio));
                if (!Directory.Exists(_ruta))
                {
                    Directory.CreateDirectory(_ruta);
                }
                return _ruta;
            }
        }
        public string RutaImagenCBB
        {
            get
            {
                return string.Format("{0}{1}{2}.{3}", _factura.Sucursal.Rfc, _factura.Serie, _factura.Folio, "BMP");
            }
        }
    }
}