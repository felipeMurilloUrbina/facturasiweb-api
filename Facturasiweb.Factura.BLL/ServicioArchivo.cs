using Facturasiweb.Factura.BLL.Servicios;
using Facturasiweb.Factura.LogDLL;
using Facturasiweb.Factura.Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.BLL
{
    public class ServicioArchivo
    {
        private Logger _logger = null;
        public ServicioArchivo(Logger logger)
        {
            this._logger = logger;
        }

        public Boolean CrearCarpeta(string ruta)
        {
            Boolean respuesta = true;
            if (!Directory.Exists(ruta))
            {
                try
                {
                    Directory.CreateDirectory(ruta);
                }
                catch (Exception e)
                {
                    _logger.EscribirError("", e.ToString());
                    respuesta = false;
                }
            }
            return respuesta;
        }
        public string GetNombre(ref Sucursal sucursal, Ruta ruta, string extension)
        {
            string _nombreArchivo = Guid.NewGuid().ToString()+ extension;
            switch (extension)
            {
                case ".cer":
                    sucursal.RutaCer = _nombreArchivo;
                    _nombreArchivo = Path.Combine(ruta.RutaCarpetaArchivosSucursal(sucursal.Id.ToString(), sucursal.Rfc, "certificados"), _nombreArchivo);
                    break;
                case ".key":
                    sucursal.RutaKey = _nombreArchivo;
                    _nombreArchivo = Path.Combine(ruta.RutaCarpetaArchivosSucursal(sucursal.Id.ToString(), sucursal.Rfc, "certificados"), _nombreArchivo);
                    break;
                default:
                    sucursal.Logo = _nombreArchivo;
                    _nombreArchivo = Path.Combine(ruta.RutaCarpetaArchivosSucursal(sucursal.Id.ToString(), sucursal.Rfc, "imagenes"), _nombreArchivo);
                    break;
            }
            return _nombreArchivo;
        }

    }
}
