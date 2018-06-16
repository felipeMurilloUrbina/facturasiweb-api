using Facturasiweb.Factura.Modelo.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo
{
    public class Direccion: Base
    {
        public int ClienteId { get; set; }
        public string Calle { get; set; }
        public string Colonia { get; set; }
        public string Ciudad { get; set; }
        public string CodigoPostal { get; set; }
        public string Municipio { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string PersonasContacto { get; set; }
        public string Correo { get; set; }
        [ForeignKey("ClienteId")]
        public virtual Cliente Cliente { get; set; }

    }
}
