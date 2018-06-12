using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo
{
    [Table("Regimenes")]
    public class Regimen: Base
    {
        public Regimen()
        {
        }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public DateTime ? FechaCreacion { get; set; }
    }
}
