using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo.PuntoVenta
{
    public partial class PuntoVenta
    {
        [NotMapped]
        public string NombreCliente
        {
            get
            {
                return Cliente == null ? "" : Cliente.Descripcion;
            }
        }
        [NotMapped]
        public Decimal Total
        {
            get
            {
                return this.Detalles == null ? 0 : Decimal.Round((this.Detalles.Sum(d => d.TotalNeto) + (this.Detalles.Sum(d => d.Iva + d.Ieps))), 2);
            }
        }
        [NotMapped]
        public Decimal Cambio
        {
            get
            {
                return  (this.DineroEnEfectivo + this.DineroEnCupones + this.DineroEnTarjeta + this.DineroEnVales) - this.Total;
            }
        }
        [NotMapped]
        public Decimal Iva
        {
            get
            {
                return this.Detalles == null ? 0 : Decimal.Round(this.Detalles.Sum(d => d.Iva), 2);
            }
        }
        [NotMapped]
        public Decimal SubtotalE
        {
            get
            {
                return this.Detalles == null ? 0 : decimal.Round(this.Detalles.Where(d => d.TasaIva == 0).Sum(d => d.TotalNeto), 2);
            }
        }
        [NotMapped]
        public Decimal SubtotalG
        {
            get
            {
                return this.Detalles == null ? 0 : this.Detalles.Where(d => d.TasaIva > 0).Sum(d => d.TotalNeto);
            }
        }
        [NotMapped]
        public Decimal Descuento
        {
            get
            {
                return this.Detalles == null ? 0 : (Decimal.Round(this.Detalles.Sum(d => d.Descuento), 2));
            }
        }
        [NotMapped]
        public Decimal Ieps
        {
            get
            {
                return this.Detalles == null ? 0 : decimal.Round(this.Detalles.Sum(d => d.Ieps), 2);
            }
        }

    }
}
