using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo
{
    [Table("Series")]
    public class Serie: Base
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int SucursalId { get; set; }
    }
}
