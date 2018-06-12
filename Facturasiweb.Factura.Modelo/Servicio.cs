using Facturasiweb.Factura.Model;
using Facturasiweb.Factura.Modelo.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo
{
    [Table("Servicios")]
    public partial class Servicio : Base, IUsuarioCreador
    {
        public Servicio()
        {
            this.Detalles = new HashSet<ServicioDetalle>();
        }
        public int EquipoId { get; set; }
        public int SucursalId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Mecanico { get; set; }
        public string Observacion { get; set; }
        public bool EstaFacturado { get; set; }

        [ForeignKey("EquipoId")]
        public virtual Equipo Equipo { get; set; }
        public virtual ICollection<ServicioDetalle> Detalles { get; set; }
        public int UsuarioCreadorId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int UsuarioModificadorId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
