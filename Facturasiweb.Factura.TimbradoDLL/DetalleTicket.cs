using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.TimbradoDLL
{
    public class DetalleTicket
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public Decimal Precio { get; set; }
        public Decimal Cantidad { get; set; }
        public Decimal Total { get; set; }
        public Decimal TasaIva { get; set; }
        public Decimal TasaIeps { get; set; }
        public Decimal TotalIva
        {
            get
            {
                return TasaIva * BaseIva;
            }
        }
        public Decimal BaseIva { get; set; }
        public Decimal BaseIeps { get; set; }
        public Decimal TotalIeps
        {
            get
            {
                return TasaIeps * BaseIeps;
            }
        }
    }
}
