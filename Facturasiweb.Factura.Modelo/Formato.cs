using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo
{
    [Table("Formatos")]
    public class Formato: Base
    {
        public string Descripcion { get; set; }
        public String ImagenMiniatura { get; set; }
        public String NombreFormato { get; set; }
    }
}
