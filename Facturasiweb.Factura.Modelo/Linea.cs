using Facturasiweb.Factura.Modelo.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo
{
    [Table("Lineas")]
    public class Linea: Base, IUsuarioCreador
    {
        public Linea()
        {
        }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int ? LineaId { get; set; }
        public int UsuarioId { get; set; }

        public virtual Linea SubLinea { get; set; }
        public int UsuarioCreadorId { get; set; }
        public int UsuarioModificadorId { get; set; }
    }
}
