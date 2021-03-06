//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Facturasiweb.Factura.Modelo
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel;
    using Facturasiweb.Factura.Modelo.Complementos;
    [Table("Facturas")]
    public partial class Factura : Base
    {
        public Factura()
        {
            this.Detalles = new HashSet<FactDetalle>();
            this.Complementos = new HashSet<Complemento>();
        }
        public string Serie { get; set; }
        public int Folio { get; set; }
        public string Tipoventa { get; set; }
        public System.DateTime Fecha { get; set; }
        public string CantidadEnLetra { get; set; }
        public int ClienteId { get; set; }
        public int MetodopagoId { get; set; }
        public int FormaPagoId { get; set; }
        public Decimal ? SubtotalGlobal { get; set; }
        public Decimal ? TotalGlobal { get; set; }
        public Decimal ? IepsGlobal { get; set; }
        public Decimal ? IvaGlobal { get; set; }
        public string Banco { get; set; }
        public string NoCuentaPago { get; set; }
        public string PagoEn { get; set; }
        public string FolioFiscal { get; set; }
        public string SelloCfd { get; set; }
        public string SelloSat { get; set; }
        public string NoCertificadoSat { get; set; }
        public string NoCertificadoEmisor { get; set; }
        public string FechaTimbrado { get; set; }
        public string VersionTimbrado { get; set; }
        public string CadenaOriginal { get; set; }
        public string ImagenCbb { get; set; }
        public string Tipo { get; set; }
        public string Observacion { get; set; }
        public string Estatus { get; set; }
        public string AcuseCancelacion { get; set; }
        public DateTime? FechaCancelacion { get; set; }
        public int SucursalId { get; set; }
        public int RegimenId { get; set; }
        public int UsoCFDIId { get; set; }
        public int FormatoId { get; set; }
        [DefaultValue(true)]
        public bool EsCredito { get; set; }
        public bool EsGlobal { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<Complemento> Complementos { get; set; }
        public virtual ICollection<FactDetalle> Detalles { get; set; }
        public virtual FormaPago FormaPago { get; set; }
        public virtual MetodoPago MetodoPago { get; set; }
        public virtual Sucursal Sucursal { get; set; }
        [ForeignKey("FormatoId")]
        public virtual Formato Formato { get; set; }
        public virtual Regimen Regimen { get; set; }
        [ForeignKey("UsoCFDIId")]
        public virtual UsoCFDI UsoCFDI { get; set; }
 
    }
}
