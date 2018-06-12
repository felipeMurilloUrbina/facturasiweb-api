using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo
{
    public partial class Sucursal
    {
        [NotMapped]
        public Byte[] LogoBytes { get; set; }
        [NotMapped]
        public Boolean EsLista
        {
            get
            {
                return !string.IsNullOrEmpty(RutaCer) && !string.IsNullOrEmpty(RutaKey) ? true : false;
            }
        }
    }
}
