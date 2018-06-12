using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo.Interfaces
{
    interface IUsuarioCreador
    {
        int UsuarioCreadorId { get; set; }
        int UsuarioModificadorId { get; set; }
    }
}
