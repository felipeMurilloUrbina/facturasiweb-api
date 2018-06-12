using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo
{
    [Table("ServicioDetalles")]
    public class ServicioDetalle: Base
    {
        public int ServicioId { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public DateTime Fecha { get; set; }
        public virtual Servicio Servicio { get; set; }
        [NotMapped]
        public Decimal TotalNeto
        {
            get
            {
                return Precio * Cantidad;
            }
        }
    }
}
