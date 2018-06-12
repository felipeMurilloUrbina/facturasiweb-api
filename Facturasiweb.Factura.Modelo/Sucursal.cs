using Facturasiweb.Factura.Modelo.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Facturasiweb.Factura.Modelo
{
    [Table("Sucursales")]
    public partial class Sucursal : Base, IUsuarioCreador
    {
        public Sucursal()
        {
            this.Regimenes = new HashSet<SucursalRegimen>();
        }
        public int? UsuarioId { get; set; }
        public int FormatoId { get; set; }
        public int ? AlmacenPrincipalId { get; set; }
        public int UsuarioCreadorId { get; set; }
        public int UsuarioModificadorId { get; set; }
        public string Descripcion { get; set; }
        public string Serie { get; set; }
        public string Calle { get; set; }
        public string Colonia { get; set; }
        public string Ciudad { get; set; }
        public string CodigoPostal { get; set; }
        public string Municipio { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string NombreComercial { get; set; }
        public string Rfc { get; set; }
        public string RutaCer { get; set; }
        public string RutaKey { get; set; }
        public string ClavePrivada { get; set; }
        public string Logo { get; set; }
        public string MensajeComercial { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
        public string ColorReporte { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; }
        [ForeignKey("UsuarioId")]
        public virtual UsuarioSistema Usuario { get; set; }
        public virtual ICollection<SucursalRegimen> Regimenes { get; set; }
        [ForeignKey("UsuarioCreadorId")]
        public virtual Usuario UsuarioCreador { get; set; }
        
    }
}
