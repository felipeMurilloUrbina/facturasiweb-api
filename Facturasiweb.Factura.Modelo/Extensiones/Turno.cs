using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo.PuntoVenta
{
    public partial class Turno
    {
        [NotMapped]
        public Decimal DineroVentas
        {
            get
            {
                return this.Ventas.Count == 0 ? 0 : this.Ventas.Sum(v => v.Total);
            }
        }
    }
}
