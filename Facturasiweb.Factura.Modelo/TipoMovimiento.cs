using Facturasiweb.Factura.Modelo.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo
{
    [Table("TiposMovimiento")]
    public class TipoMovimiento: Base, IUsuarioCreador
    {
        public string Tipo { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Clasificacion { get; set; }
        public int UsuarioSistemaId { get; set; }
        public int UsuarioCreadorId { get; set; }
        public int UsuarioModificadorId { get; set; }
    }
}
