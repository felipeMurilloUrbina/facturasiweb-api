using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Model.Inventario
{
    public class AlmacenProducto
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public decimal Stock { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioId { get; set; }
    }
}
