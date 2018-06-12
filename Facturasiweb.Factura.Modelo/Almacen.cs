using Facturasiweb.Factura.Modelo.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo
{
    [Table("Almacenes")]
    public class Almacen : Base, IUsuarioCreador
    {
        public int SucursalId { get; set; }
        public int UsuarioId { get; set; }
        public int UsuarioCreadorId { get; set; }
        public int UsuarioModificadorId { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }

        public virtual Sucursal Sucursal { get; set; }
        public virtual Usuario UsuarioSistema { get; set; }
    }
}
