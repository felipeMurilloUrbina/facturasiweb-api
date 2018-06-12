using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo
{
    [Table("ComplementoPagos")]
    public class ComplementoPago
    {
        [Key]
        public int Id { get; set; }
        public string Serie { get; set; }
        public int FacturaId { get; set; }
        public decimal Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public int UsuarioId { get; set; }
        [ForeignKey("FacturaId")]
        public virtual Factura Factura { get; set; }
    }
}
