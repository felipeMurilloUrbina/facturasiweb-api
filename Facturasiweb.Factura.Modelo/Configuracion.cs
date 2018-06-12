using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo
{
    [Table("Configuraciones")]
    public class Configuracion
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int Plantilla { get; set; }
        public int TimbresComprados { get; set; }
        public int TimbresUsados { get; set; }
        public String Asunto { get; set; }
        public String Mensaje { get; set; }

        public int TimbresDisponibles
        {
            get
            {
                return  TimbresComprados>0 ? TimbresComprados - TimbresUsados: 0;
            }
        }
        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }
    }
}
