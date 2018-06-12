using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Facturasiweb.Factura.Modelo.PuntoVenta
{
    public partial class PuntoVentaDetalle
    {
        [NotMapped]
        public int TasaIvaMostrar
        {
            get
            {
                return (Int32)(100 * TasaIva);
            }
        }
        [NotMapped]
        public int TasaIepsaMostrar
        {
            get
            {
                return (Int32)(100 * TasaIeps);
            }
        }
        
        [NotMapped]
        public Decimal Total
        {
            get
            {
                return decimal.Round((Cantidad * Precio), 2);
            }
        }
        [NotMapped]
        public Decimal Iva
        {
            get
            {

                return TasaIva == 0 ? 0 : decimal.Round(((TotalNeto) * (decimal)TasaIva), 2);
            }
        }
        [NotMapped]
        public Decimal Ieps
        {
            get
            {
                return TasaIeps == 0 ? 0 : decimal.Round(((TotalNeto) * ((decimal)TasaIeps)), 2);
            }
        }
        [NotMapped]
        public Decimal Descuento
        {
            get
            {
                return TasaDesc == 0 ? 0 : (Total) * (TasaDesc / 100);
            }
        }
        [NotMapped]
        public Decimal TotalNeto
        {
            get
            {
                return decimal.Round((Total - Descuento), 2);
            }
        }
    }
}
