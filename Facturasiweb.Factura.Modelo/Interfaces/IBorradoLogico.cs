using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo.Interfaces
{
    public class IBorradoLogico
    {
        [DefaultValue(true)]
        public Boolean Activo { get; set; }
    }
}
