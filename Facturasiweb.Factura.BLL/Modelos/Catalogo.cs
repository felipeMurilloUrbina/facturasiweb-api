using Facturasiweb.Factura.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.BLL.Modelos
{
    public class Catalogo
    {
        public ICollection<Pais> Paises { get; set; }
        public ICollection<Regimen> Regimenes { get; set; }
        public ICollection<Formato> Formatos { get; set; }
    }
}
