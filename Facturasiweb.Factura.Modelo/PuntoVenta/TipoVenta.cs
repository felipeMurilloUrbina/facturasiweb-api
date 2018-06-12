using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Facturasiweb.Factura.Modelo.PuntoVenta
{
    [Table("TipoVentas")]
    public class TipoVenta :Base
    {
        public string Descripcion { get; set; }
        public bool CapturaInformacion { get; set; }
    }
}
