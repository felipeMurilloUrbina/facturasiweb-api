using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo.Complementos
{
    [Table("Complementos")]
    public class Complemento
    {
        public int Id { get; set; }
        public int FacturaId { get; set; }
        public string Nombre { get; set; }
        public string Detalle { get; set; }

    }
}
