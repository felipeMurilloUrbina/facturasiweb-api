using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo.DTO
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string Contra { get; set; }
        public bool Activo { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
        public string Tipo { get; set; }
        public String Asunto { get; set; }
        public String Mensaje { get; set; }
        public string NombreUsuario { get; set; }
        public string Avatar { get; set; }
        public bool ConexionShya { get; set; }
        public String NombreCompleto { get; set; }
        public int TimbresComprados { get; set; }
        public int TimbresUsados { get; set; }
        public int TimbresDisponibles { get; set; }
        public int? SucursalId { get; set; }
        public int? ClienteId { get; set; }
        public int UsuarioSistemaId { get; set; }
    }
}
