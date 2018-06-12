using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Modelo
{
    public partial class Cotizacion
    {
        [NotMapped]
        public String Codigo { get { return GetCodigo(Serie, Folio); } }
        [NotMapped]
        public Decimal Iva
        {
            get
            {
                return Decimal.Round(this.Detalles.Sum(d => d.Iva), 2);
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
                return Decimal.Round(this.Detalles.Where(d => d.TasaIva == 0).Sum(d => d.TotalNeto), 2);
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
                return Decimal.Round(this.Detalles.Sum(d => d.Ieps), 2);
            }
        }
        [NotMapped]
        public Decimal RetIva
        {
            get
            {
                return Decimal.Round(this.Detalles.Sum(d => d.RetIvaDinero), 2);
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
                return Decimal.Round(((this.Importe - (this.RetIva + this.RetIsr)) + (this.Ieps + this.Iva)), 2);
            }
        }

        public string GetCodigo(string serie, int folio)
        {
            string _codigo = "";
            switch (folio.ToString().Length)
            {
                case 1:
                    _codigo = serie+"-00000" + folio;
                    break;
                case 2:
                    _codigo = serie + "-0000" + folio;
                    break;
                case 3:
                    _codigo = serie + "-000" + folio;
                    break;
                case 4:
                    _codigo = serie + "-00" + folio;
                    break;
                case 5:
                    _codigo = serie + "-0" + folio;
                    break;
                case 6:
                    _codigo = serie + "-" + folio;
                    break;
            }
            return _codigo;
        }

    }
}
