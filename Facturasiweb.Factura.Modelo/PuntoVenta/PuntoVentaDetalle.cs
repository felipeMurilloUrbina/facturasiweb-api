using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Facturasiweb.Factura.Modelo.PuntoVenta
{
    [Table("PuntoVentaDetalles")]
    public partial class PuntoVentaDetalle: Base
    {
        public int PuntoVentaId { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public Decimal Cantidad { get; set; }
        public Decimal Precio { get; set; }
        public Decimal TasaIva { get; set; }
        public Decimal TasaIeps { get; set; }
        public Decimal TasaDesc { get; set; }
        public bool AfectaInv { get; set; }
    }
}
