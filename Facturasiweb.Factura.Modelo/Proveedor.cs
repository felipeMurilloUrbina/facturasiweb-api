using Facturasiweb.Factura.Modelo.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo
{
    [Table("Proveedores")]
    public class Proveedor: Base, IUsuarioCreador
    {
        public int UsuarioId { get; set; }
        public int UsuarioCreadorId { get; set; }
        public int UsuarioModificadorId { get; set; }
        public string Descripcion { get; set; }
        public string Calle { get; set; }
        public string Colonia { get; set; }
        public string Ciudad { get; set; }
        public string CodigoPostal { get; set; }
        public string Municipio { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Rfc { get; set; }
        public string Correo { get; set; }
        [ForeignKey("UsuarioId")]
        public virtual UsuarioSistema Usuario { get; set; }
    }
}
