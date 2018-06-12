using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo
{
    public partial class DetalleCotizacion
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
        public int RetIvaMostrar
        {
            get
            {
                return (Int32)(100 * TasaRetIva);
            }
        }
        [NotMapped]
        public int RetIsrMostrar
        {
            get
            {
                return (Int32)(100 * TasaRetIsr);
            }
        }
        [NotMapped]
        public string CodigoSat { get; set; }
        [NotMapped]
        public Decimal Total
        {
            get
            {
                return (Cantidad * Precio);
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
                return TasaDesc == 0 ? 0 : decimal.Round((Total) * (TasaDesc / 100), 2);
            }
        }
        [NotMapped]
        public Decimal RetIvaDinero
        {
            get
            {
                return TasaRetIva == 0 ? 0 : decimal.Round((TotalNeto * TasaRetIva), 2);
            }
        }
        [NotMapped]
        public Decimal RetIsrDinero
        {
            get
            {
                return TasaRetIsr == 0 ? 0 : decimal.Round((TotalNeto * TasaRetIsr), 2);
            }
        }
        [NotMapped]
        public Decimal TotalNeto
        {
            get
            {
                return (Total - Descuento);
            }
        }

    }
}
