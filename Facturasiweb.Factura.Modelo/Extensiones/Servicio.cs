using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo
{
    public partial class Servicio
    {
        [NotMapped]
        public Decimal Total
        {
            get
            {
                return this.Detalles.Sum(d => d.TotalNeto);
            }
        }
    }
}
