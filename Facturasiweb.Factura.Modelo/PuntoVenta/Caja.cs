using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo.PuntoVenta
{
    public class Caja: Base
    {
        public int SucursalId { get; set; }
        public int UsuarioId { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }
        [ForeignKey("SucursalId")]
        public Sucursal Sucursal { get; set; }
    }
}
