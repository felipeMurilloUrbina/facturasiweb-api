using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo
{
    public partial class Factura
    {
        [NotMapped]
        public Byte[] ImagenCbbBytes { get; set; }
        [NotMapped]
        public String Codigo { get { return string.Format("{0}-{1}", Serie, Folio); } }
        [NotMapped]
        public string NombreCliente
        {
            get
            {
                return Cliente == null ? "" : Cliente.Descripcion;
            }
        }
        [NotMapped]
        public Decimal  Iva 
        {
            get
            {
                return   Decimal.Round(this.Detalles.Sum(d => d.Iva), 2);
            }
            set
            {

            }
        }
        [NotMapped]
        public Decimal SubtotalE
        {
            get
            {
                return  Decimal.Round(this.Detalles.Where(d => d.TasaIva == 0).Sum(d => d.TotalNeto), 2);
            }
        }
        [NotMapped]
        public Decimal SubtotalG
        {
            get
            {
                return Decimal.Round(this.Detalles.Where(d => d.TasaIva > 0).Sum(d => d.TotalNeto), 2);
            }
        }
        [NotMapped]        
        public Decimal Importe
        {
            get
            {
                return Decimal.Round((this.SubtotalE + this.SubtotalG), 2);
            }
        }
        [NotMapped]
        public Decimal Descuento
        {
            get
            {
                return Decimal.Round(this.Detalles.Sum(d => d.Descuento), 2);
            }
        }
        [NotMapped]        
        public Decimal Ieps
        {
            get
            {
                return  Decimal.Round(this.Detalles.Sum(d => d.Ieps), 2);
            }
        }
        [NotMapped]
        public Decimal RetIva
        {
            get
            {
                return Decimal.Round(this.Detalles.Sum(d => d.RetIvaDinero),2);
            }
        }
        [NotMapped]        
        public Decimal RetIsr
        {
            get
            {
                return Decimal.Round(this.Detalles.Sum(d => d.RetIsrDinero), 2);
            }
        }
        [NotMapped]
        public Decimal Total
        {
            get
            {
                return  Decimal.Round(((this.Importe - (this.RetIva + this.RetIsr)) + (this.Ieps + this.Iva)), 2);
            }
        }
    }
}
