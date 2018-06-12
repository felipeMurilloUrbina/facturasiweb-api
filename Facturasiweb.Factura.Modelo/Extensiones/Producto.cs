using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo
{
    public partial class Producto
    {
        [NotMapped]
        public int TasaIvaMostrar
        {
            get
            {
                return (Int32)(100 * TasaIva);
            }
        }
        [NotMapped]
        public int TasaIepsMostrar
        {
            get
            {
                return (Int32)(100 * TasaIeps);
            }
        }
        [NotMapped]
        public int TasaRetIvaMostrar
        {
            get
            {
                return (Int32)(100 * TasaRetIva);
            }
        }
        [NotMapped]
        public int TasaRetIsrMostrar
        {
            get
            {
                return (Int32)(100 * TasaRetIsr);
            }
        }
    }
}
