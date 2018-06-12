using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo
{
    [Table("SucursalRegimenes")]
    public class SucursalRegimen
    {
        [Key]
        [Column(Order = 1)]
        public int SucursalId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int RegimenId { get; set; }
        public bool IsDefault { get; set; }
        [ForeignKey("RegimenId")]
        public virtual Regimen Regimen { get; set; }
    }
}
