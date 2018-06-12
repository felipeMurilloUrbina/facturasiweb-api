using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Model.Inventario
{
    public class EntradaDetalle
    {
        public int EntradaId { get; set; }
        public int ProductoId { get; set; }
        public Decimal Cantidad { get; set; }
        public Decimal Costo { get; set; }
    }
}
