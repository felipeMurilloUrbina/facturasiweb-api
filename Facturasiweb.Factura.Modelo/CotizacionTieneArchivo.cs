using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo
{
    [Table("CotizacionTieneArchivos")]
    public class CotizacionTieneArchivo : Base
    {
        public int CotizacionId { get; set; }
        public int UsuarioId{ get; set; }
        public string NombreFisico { get; set; }
        public string NombreHistorico { get; set; }

        [ForeignKey("CotizacionId")]
        public virtual Cotizacion Cotizacion { get; set; }
    }
}
