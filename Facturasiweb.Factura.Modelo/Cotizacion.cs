using Facturasiweb.Factura.Modelo.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo
{
    [Table("Cotizaciones")]
    public partial class Cotizacion : Base, IUsuarioCreador
    {
        public Cotizacion()
        {
            this.Detalles = new HashSet<DetalleCotizacion>();
            this.Archivos = new HashSet<CotizacionTieneArchivo>();
        }
        public int ClienteId { get; set; }
        public int SucursalId { get; set; }
        public int UsuarioId { get; set; }
        public int FormaPagoId { get; set; }
        public int FormatoId { get; set; }
        public int MetodoPagoId { get; set; }
        public int RegimenId { get; set; }
        public int EstatusId { get; set; }
        public int UsuarioCreadorId { get; set; }
        public int UsuarioModificadorId { get; set; }
        public int Folio { get; set; }
        public string Serie { get; set; }
        public string CantidadEnLetra { get; set; }
        public string Banco { get; set; }
        public string NoCuentaBanco { get; set; }
        public Boolean EsCredito { get; set; }
        public string Observacion { get; set; }
        public DateTime Fecha { get; set; }
        public virtual Sucursal Sucursal { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual MetodoPago MetodoPago { get; set; }
        public virtual FormaPago FormaPago { get; set; }
        public virtual Formato Formato { get; set; }
        [ForeignKey("RegimenId")]
        public virtual Regimen Regimen { get; set; }
        [ForeignKey("EstatusId")]
        public virtual Estatus Estatus { get; set; }
        public virtual ICollection<DetalleCotizacion> Detalles { get; set; }
        public virtual ICollection<CotizacionTieneArchivo> Archivos { get; set; }
    }
}
