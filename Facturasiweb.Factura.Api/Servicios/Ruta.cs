using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Facturasiweb.Factura.Api.Servicios
{
    //public static class Ruta
    //{
    //    public static string RutaError = "~/App_Data/errores";
    //    public static string RutaCarpetaCertificados = "~/App_Data/archivos/{0}-{1}/certificados";
    //    public static string RutaCertificados = "~/App_Data/archivos/{0}-{1}/certificados/{2}";
    //    
    //    public static string RutaError = "~/App_Data/errores";
    //}
    public class Ruta
    {
        public static string RutaCarpetaCertificados = "~/App_Data/archivos/{0}-{1}/certificados";
        public static string RutaCertificados = "~/App_Data/archivos/{0}-{1}/certificados/{2}";
        public static string RutaCarpetaTemp = "~/App_Data/temp/{1}";
        public static string RutaCarpetaSucursal = "~/App_Data/archivos/{0}-{1}";
        public static string RutaTempArchivo = "~/App_Data/temp/{1}";
        public static string NombreCFDITemp = "CFDI33.xml";
        public static string RutaCarpetaFacturas = "~/App_Data/archivos/{0}-{1}/facturas";
        public static string RutaCarpetaError = "~/App_Data/archivos/{0}-{1}/errores";
        public static string RutaCarpetaComplementos = "~/App_Data/archivos/{0}-{1}/complementos";
        public static string RutaArchivos = "~/App_Data/archivos/{0}-{1}/{5}/{1}{2}-{3}.{4}";
        public static string RutaCarpetaImagenes = "~/App_Data/archivos/{0}-{1}/imagenes";
        public static string RutaImagenes = "~/App_Data/archivos/{0}-{1}/imagenes/{2}";
        public static string RutaError = "~/App_Data/errores";
        HttpContext _contexto = null;
        Modelo.Factura _factura = null;
        public Ruta(HttpContext _contexto, Modelo.Factura factura)
        {
            this._contexto = _contexto;
            this._factura = factura;
        }
        public Ruta(HttpContext _contexto)
        {
            this._contexto = _contexto;
        }
        public string RutaArchivosSucursal(string id, string rfc, string nombre)
        {

                return _contexto.Server.MapPath(string.Format(RutaCertificados, id, rfc, nombre));
        }

        public string RutaCarpetaErrorUsuario
        {
            get
            {

                return _contexto.Server.MapPath(string.Format(RutaCarpetaError, _factura.Sucursal.Id, _factura.Sucursal.Rfc));
            }
        }

        public string RutaXML
        {
            get
            {
                return _contexto.Server.MapPath(String.Format(RutaArchivos, _factura.SucursalId, _factura.Sucursal.Rfc, _factura.Serie, _factura.Folio, "xml", "facturas"));
            }
        }

        public string RutaXMLTemporal
        {
            get
            {
                return Path.Combine(_contexto.Server.MapPath(string.Format(RutaCarpetaFacturas, _factura.Sucursal.Id, _factura.Sucursal.Rfc)), NombreCFDITemp); //_contexto.Server.MapPath(String.Format(Ruta.RutaArchivos, _factura.SucursalId, _factura.Sucursal.Rfc, _factura.Serie, _factura.Folio, "xml", "_facturas"));
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
                return _contexto.Server.MapPath(string.Format(RutaCertificados, _factura.Sucursal.Id, _factura.Sucursal.Rfc, _factura.Sucursal.RutaKey));
            }
        }
        public string RutaCer
        {
            get
            {
                return _contexto.Server.MapPath(string.Format(RutaCertificados, _factura.Sucursal.Id, _factura.Sucursal.Rfc, _factura.Sucursal.RutaCer));
            }
        }
        public string RutaCarpetaFacturasCliente
        {
            get
            {
                if (!Directory.Exists(_contexto.Server.MapPath(string.Format(RutaCarpetaFacturas, _factura.Sucursal.Id, _factura.Sucursal.Rfc))))
                {
                    Directory.CreateDirectory(_contexto.Server.MapPath(string.Format(RutaCarpetaFacturas, _factura.Sucursal.Id, _factura.Sucursal.Rfc)));
                }
                return _contexto.Server.MapPath(string.Format(RutaCarpetaFacturas, _factura.Sucursal.Id, _factura.Sucursal.Rfc));
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