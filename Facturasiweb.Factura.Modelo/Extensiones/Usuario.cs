using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo
{
    public partial class Usuario
    {
        [NotMapped]
        public String NombreCompleto
        {
            get
            {
                return string.Format("{0} {1}", Nombre, ApellidoPaterno);
            }
        }
        [NotMapped]
        public int TimbresDisponibles
        {
            get
            {
                return TimbresComprados > 0 ? TimbresComprados - TimbresUsados : 0;

            }
        }
    }
}