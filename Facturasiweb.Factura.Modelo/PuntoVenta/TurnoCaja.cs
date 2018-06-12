using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo.PuntoVenta
{
    [Table("TurnoCajas")]
   public class TurnoCaja
    {
        [Key]
        [Column(Order = 1)]
        public int TurnoId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int CajaId { get; set; }
        [Key]
        [Column(Order = 3)]
        public int SucursalId { get; set; }
        [Key]
        [Column(Order = 4)]
        public int UsuarioId { get; set; }
        public decimal FondoCaja { get; set; }
        public bool Activo { get; set; }
        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }
        [ForeignKey("SucursalId")]
        public virtual Sucursal Sucursal { get; set; }
    }
}
