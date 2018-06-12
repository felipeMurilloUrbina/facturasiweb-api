using Facturasiweb.Factura.Modelo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo
{
    public class Base : IBorradoLogico
    {
        public int Id { get; set; }
        public new Boolean Activo { get; set; } = true;
    }
}
