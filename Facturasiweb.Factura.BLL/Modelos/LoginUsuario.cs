using Facturasiweb.Factura.Model;
using Facturasiweb.Factura.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.BLL.Modelos
{
    public class LoginUsuario
    {
        public String Token { get; set; }
        public ICollection<Sucursal> Sucursales { get; set; }
    }
}
