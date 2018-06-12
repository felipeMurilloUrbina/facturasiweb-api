using Facturasiweb.Factura.Modelo.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo
{
    [Table("Equipos")]
    public class Equipo: Base, IUsuarioCreador
    {
        public int? ClienteId { get; set; }
        public int UsuarioId { get; set; }
        public String Descripcion { get; set; }
        public string Anio { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public string Imagen { get; set; }
        public string Observacion { get; set; }
        public bool EsPropio { get; set; }
        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }
        public int UsuarioCreadorId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int UsuarioModificadorId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
