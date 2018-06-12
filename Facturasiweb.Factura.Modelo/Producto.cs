using Facturasiweb.Factura.Modelo.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo
{
    [Table("Productos")]
    public partial class Producto: Base, IUsuarioCreador
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string CuentaPredial { get; set; }
        public string FechaCaducidad { get; set; }
        public string Ubicacion { get; set; }
        public string Observacion { get; set; }
        public string Lote { get; set; }
        public decimal ? Precio { get; set; }
        public decimal ? Costo { get; set; }
        public int Descuento { get; set; }
        public Decimal TasaIva { get; set; }
        public Decimal TasaIeps { get; set; }
        public Decimal TasaRetIva { get; set; }
        public Decimal TasaRetIsr { get; set; }
        public int CatalogoId { get; set; }
        public int UnidadId { get; set; }
        public int UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public virtual UsuarioSistema Usuario { get; set; }
        [ForeignKey("CatalogoId")]
        public virtual CatSatProducto CatalogoSat { get; set; }
        [ForeignKey("UnidadId")]
        public virtual CatSatUnidad CatSatUnidad { get; set; }
        public int UsuarioCreadorId { get; set; }
        public int UsuarioModificadorId { get; set; }
    }
}
