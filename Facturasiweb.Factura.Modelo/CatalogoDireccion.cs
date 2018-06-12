using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo
{
    [Table("CatalogoDirecciones")]
    public class CatalogoDireccion: Base
    {
        public string Estado { get; set; }
        public string Municipio { get; set; }
        public string Localidad { get; set; }
        public string CodigoPostal { get; set; }
    }
}
