using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Model.Inventario
{
    public class Entrada
    {
        public int Id { get; set; }
        public int SucursalId { get; set; }
        public int AlmacenId { get; set; }
        public DateTime Fecha { get; set; }
        public string Serie { get; set; }
        public int Folio { get; set; }
        public bool Activo { get; set; }
    }
}
