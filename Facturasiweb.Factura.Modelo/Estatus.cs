using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo
{
    [Table("Estatus")]
    public partial class Estatus: Base
    {
        public string Descripcion { get; set; }
    }
}
