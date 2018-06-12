using Facturasiweb.Factura.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.BLL.Modelos
{
    public class FacturaNueva
    {
        public string Serie { get; set; }
        public int ? Folio { get; set; }
        public ICollection<SucursalRegimen> Regimenes { get; set; }
    }
}
