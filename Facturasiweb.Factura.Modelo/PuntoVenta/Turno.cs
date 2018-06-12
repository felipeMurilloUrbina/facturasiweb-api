using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo.PuntoVenta
{
    [Table("Turnos")]
    public partial class Turno: Base
    {
        public Turno()
        {
            this.Cajas = new HashSet<TurnoCaja>();
            this.Ventas = new HashSet<PuntoVenta>();
        }
        public int Folio { get; set; }
        public int SucursalId { get; set; }
        public int UsuarioId { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime ? Fin { get; set; }
        public bool Estatus { get; set; }
        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }
        [ForeignKey("SucursalId")]
        public virtual Sucursal Sucursal { get; set; }
        public virtual ICollection<TurnoCaja> Cajas { get; set; }
        public virtual ICollection<PuntoVenta> Ventas { get; set; }
    }
}
