using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo
{
    [Table("UsuariosSistema")]
    public class UsuarioSistema: Base
    {
        public UsuarioSistema()
        {
            this.Sucursales = new HashSet<Sucursal>();
        }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string NombreUsuario { get; set; }
        public string Contra { get; set; }
        public virtual ICollection<Sucursal> Sucursales { get; set; }

    }
}
