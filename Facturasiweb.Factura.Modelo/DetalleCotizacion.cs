using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo
{
    [Table("DetalleCotizaciones")]
    public partial class DetalleCotizacion: Base
    {
        public int CotizacionId { get; set; }
        public int ProductoId { get; set; }
        public int ? CatalogoId { get; set; }
        public int ? UnidadId { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Lote { get; set; }
        public string FechaCaducidad { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal TasaIva { get; set; }
        public decimal TasaIeps { get; set; }
        public decimal TasaRetIva { get; set; }
        public decimal TasaRetIsr { get; set; }
        public decimal TasaDesc { get; set; }
        [ForeignKey("CatalogoId")]
        public virtual CatSatProducto CatSatProducto { get; set; }
        [ForeignKey("UnidadId")]
        public virtual CatSatUnidad CatSatUnidad { get; set; }
        public virtual Cotizacion Cotizacion { get; set; }
    }
}
