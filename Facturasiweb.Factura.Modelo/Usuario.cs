using Facturasiweb.Factura.Modelo;
using Facturasiweb.Factura.Modelo.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Facturasiweb.Factura.Modelo
{
    [Table("Usuarios")]
    public partial class Usuario : Base, IUsuarioCreador
    {
        public int? SucursalId { get; set; }
        public int? ClienteId { get; set; }
        public int  UsuarioSistemaId { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string Contra { get; set; }
        public bool ConexionShya { get; set; }
        public System.DateTime FechaCreacion { get; set; }
        public string Tipo { get; set; }
        public string NombreUsuario { get; set; }
        public int Plantilla { get; set; }
        public String Avatar { get; set; }
        public int TimbresComprados { get; set; }
        public int TimbresUsados { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
        [ForeignKey("UsuarioSistemaId")]
        public virtual UsuarioSistema UsuarioSistema { get; set; }
        public int UsuarioCreadorId { get; set; }
        public int UsuarioModificadorId { get; set; }
    }
}
