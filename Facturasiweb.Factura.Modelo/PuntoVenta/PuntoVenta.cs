using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo.PuntoVenta
{
    [Table("PuntoVentas")]
    public partial class PuntoVenta : Base
    {
        public int TurnoId { get; set; }
        public int CajaId { get; set; }
        public int ClienteId { get; set; }
        public int  ? UsuarioId { get; set; }
        public int ? SucursalId { get; set; }
        public int TipoVentaId { get; set; }
        public int Folio { get; set; }
        public string Serie { get; set; }
        public decimal DineroEnTarjeta { get; set; }
        public decimal DineroEnVales { get; set; }
        public decimal DineroEnEfectivo { get; set; }
        public decimal DineroEnCupones { get; set; }
        public DateTime Fecha { get; set; }
         [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }
         [ForeignKey("SucursalId")]
        public virtual Sucursal Sucursal { get; set; }
         [ForeignKey("TurnoId")]
        public virtual Turno Turno { get; set; }
         [ForeignKey("CajaId")]
        public virtual Caja Caja { get; set; }
         [ForeignKey("ClienteId")]
        public virtual Cliente Cliente { get; set; }
         [ForeignKey("TipoVentaId")]
        public virtual  TipoVenta TipoVenta { get; set; }
        public virtual ICollection<PuntoVentaDetalle> Detalles { get; set; }
    }
}
       
