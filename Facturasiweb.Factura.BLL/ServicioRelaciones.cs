using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facturasiweb.Factura.DAO;
using Facturasiweb.Factura.LogDLL;
using Facturasiweb.Factura.Modelo;

namespace Facturasiweb.Factura.BLL
{
    public class ServicioRelaciones : ServicioBase
    {
        public ServicioRelaciones(Logger logger, Context contexto) : base(logger, contexto)
        {
        }

        public void ObtenerRelacionFactura(ref Modelo.Factura factura)
        {
            try
            {
                int id = factura.FormatoId == 0 ? 1 : factura.FormatoId;
                factura.Formato = factura.Formato ?? _contexto.Formatos.Where(f => f.Id == id).FirstOrDefault();
                id = factura.SucursalId;
                var _serie = factura.Serie;
                var _facturas = this._contexto.Facturas.Where(f => f.SucursalId == id && f.Serie.Equals(_serie)).ToList();
                var _folio = _facturas.Count() == 0 || _facturas == null ? 0 : _facturas.Max(f => f.Folio);
                _folio++;
                factura.Folio = (int)_folio;
                factura.Sucursal = _contexto.Sucursales.Where(f => f.Id == id).FirstOrDefault();
                id = factura.RegimenId;
                factura.Regimen = factura.Regimen ?? _contexto.Regimenes.Where(f => f.Id == id).FirstOrDefault();
                id = factura.ClienteId;
                factura.Cliente = factura.Cliente ?? _contexto.Clientes.Where(f => f.Id == id).FirstOrDefault();
                id = factura.MetodopagoId;
                factura.MetodoPago = factura.MetodoPago ?? _contexto.MetodoPagos.Where(f => f.Id == id).FirstOrDefault();
                id = factura.FormaPagoId;
                factura.FormaPago = factura.FormaPago ?? _contexto.FormaPagos.Where(f => f.Id == id).FirstOrDefault();
                id = factura.UsoCFDIId;
                factura.UsoCFDI = factura.UsoCFDI ?? _contexto.UsoCFDIs.Where(f => f.Id == id).FirstOrDefault();

                foreach (var detalle in factura.Detalles)
                {
                    detalle.CatSatProducto =  _contexto.CatSatProductos.Find(detalle.CatalogoId);
                    detalle.CatSatUnidad = _contexto.CatSatUnidades.Find(detalle.UnidadId);
                }
            }
            catch (Exception e)
            {
                this._logger.EscribirError(e.ToString());
            }
        }

        public void SanitizarFactura(ref Modelo.Factura factura)
        {
            factura.Cliente = null;
            factura.Regimen = null;
            factura.MetodoPago = null;
            factura.FormaPago = null;
            factura.Sucursal = null;
            factura.Formato = null;
            factura.UsoCFDI = null;
            foreach (var detalle in factura.Detalles)
            {
                detalle.CatSatProducto = null;
                detalle.CatSatUnidad = null;
            }
        }

        public void SanitizarCotizacion(ref Cotizacion cotizacion)
        {
            cotizacion.FormaPago = null;
            cotizacion.MetodoPago = null;
            cotizacion.Usuario = null;
            cotizacion.Sucursal = null;
            cotizacion.Cliente = null;
        }
    }
}
