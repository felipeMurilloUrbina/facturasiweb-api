using Facturasiweb.Factura.LogDLL;
using Facturasiweb.Factura.Modelo;
using Facturasiweb.Factura.Modelo.Complementos;
using Facturasiweb.Factura.TimbradoDLL.Cancelacion;
using Facturasiweb.Factura.TimbradoDLL.Facturacion;
using Facturasiweb.Factura.TimbradoDLL.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ThoughtWorks.QRCode.Codec;

namespace Facturasiweb.Factura.TimbradoDLL
{
    public class ServicioXml
    {
        Logger _logger = null;
        public String SelloDigital { get; set; }
        public String Certificado { get; set; }
        public ServicioXml(Logger logger)
        {
            this._logger = logger;
        }
        public XmlTextWriter XmlCreador { get; set; }
        public Boolean GenerarEncabezado(ref string error, string ruta, Modelo.Factura factura)
        {
            try
            {
                if (File.Exists(ruta))
                    File.Delete(ruta);
                XmlCreador = new XmlTextWriter(ruta, Encoding.UTF8);
                XmlCreador.WriteStartDocument(); //empezamos a crear el xml
                XmlCreador.WriteStartElement("cfdi:Comprobante"); //definimos el elemento principal comprobante
                XmlCreador.WriteStartAttribute("xmlns:xsi");
                XmlCreador.WriteValue("http://www.w3.org/2001/XMLSchema-instance");
                XmlCreador.WriteEndAttribute();
                XmlCreador.WriteStartAttribute("xsi:schemaLocation");
                XmlCreador.WriteValue("http://www.sat.gob.mx/cfd/3  http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd http://www.sat.gob.mx/iedu http://www.sat.gob.mx/sitio_internet/cfd/iedu/iedu.xsd");
                XmlCreador.WriteEndAttribute();
                XmlCreador.WriteStartAttribute("Version");
                XmlCreador.WriteValue("3.3");
                XmlCreador.WriteEndAttribute();
                XmlCreador.WriteStartAttribute("Serie");
                XmlCreador.WriteValue(factura.Serie);
                XmlCreador.WriteEndAttribute();
                XmlCreador.WriteStartAttribute("Folio");
                XmlCreador.WriteValue(factura.Folio);
                XmlCreador.WriteEndAttribute();
                XmlCreador.WriteStartAttribute("Fecha");
                XmlCreador.WriteValue(Convert.ToDateTime(String.Format("{0:s}", factura.Fecha)));
                XmlCreador.WriteEndAttribute();
                XmlCreador.WriteStartAttribute("Sello");
                XmlCreador.WriteValue(this.SelloDigital);
                XmlCreador.WriteEndAttribute();
                XmlCreador.WriteStartAttribute("FormaPago");
                XmlCreador.WriteValue(factura.FormaPago.Codigo);
                XmlCreador.WriteEndAttribute();
                XmlCreador.WriteStartAttribute("NoCertificado");
                XmlCreador.WriteValue(factura.NoCertificadoEmisor);
                XmlCreador.WriteEndAttribute();
                XmlCreador.WriteStartAttribute("Certificado");
                XmlCreador.WriteValue(this.Certificado);
                XmlCreador.WriteEndAttribute();
                XmlCreador.WriteStartAttribute("SubTotal");
                XmlCreador.WriteValue(factura.Detalles.Sum(f => f.Total).ToString("F", CultureInfo.InvariantCulture));
                XmlCreador.WriteEndAttribute();
                XmlCreador.WriteStartAttribute("Descuento");
                XmlCreador.WriteValue(factura.Detalles.Sum(d => d.Descuento).ToString("F", CultureInfo.InvariantCulture));
                XmlCreador.WriteEndAttribute();
                XmlCreador.WriteStartAttribute("Moneda");
                XmlCreador.WriteValue("MXN");
                XmlCreador.WriteEndAttribute();
                XmlCreador.WriteStartAttribute("Total");
                XmlCreador.WriteValue(factura.Total.ToString("F", CultureInfo.InvariantCulture));
                XmlCreador.WriteEndAttribute();
                XmlCreador.WriteStartAttribute("TipoDeComprobante");
                XmlCreador.WriteValue(factura.Tipo);
                XmlCreador.WriteEndAttribute();
                XmlCreador.WriteStartAttribute("MetodoPago");
                XmlCreador.WriteValue(factura.MetodoPago.Codigo);
                XmlCreador.WriteEndAttribute();
                XmlCreador.WriteStartAttribute("LugarExpedicion");
                XmlCreador.WriteValue(factura.Sucursal.CodigoPostal);
                XmlCreador.WriteEndAttribute();
                XmlCreador.WriteStartAttribute("xmlns:cfdi");
                XmlCreador.WriteValue("http://www.sat.gob.mx/cfd/3");
                XmlCreador.WriteEndAttribute();
                if (factura.Complementos.Count > 0)
                {
                    XmlCreador.WriteStartAttribute("xmlns:ine");
                    XmlCreador.WriteValue("http://www.sat.gob.mx/ine");
                    XmlCreador.WriteEndAttribute();
                }
                //XmlCreador.WriteStartAttribute("xmlns:pago10");
                //XmlCreador.WriteValue("http://www.sat.gob.mx/Pagos");
                //XmlCreador.WriteEndAttribute();
                XmlCreador.WriteStartAttribute("xmlns:iedu");
                XmlCreador.WriteValue("http://www.sat.gob.mx/iedu");
                XmlCreador.WriteEndAttribute();
                return true;
            }
            catch (Exception e)
            {
                error = "Ocurrio un error al generar nodo Encabezado";
                _logger.EscribirError(e.ToString());
                GenerarFin();
                return false;
            }
        }
        public Boolean GenerarEmisor(ref string error, Sucursal sucursal, Regimen regimen)
        {
            try
            {
                XmlCreador.WriteStartElement("cfdi:Emisor");
                XmlCreador.WriteStartAttribute("Rfc");
                XmlCreador.WriteValue(sucursal.Rfc);
                XmlCreador.WriteEndAttribute();
                XmlCreador.WriteStartAttribute("Nombre");
                XmlCreador.WriteValue(sucursal.Descripcion.Replace("&", "&amp;"));
                XmlCreador.WriteEndAttribute();
                XmlCreador.WriteStartAttribute("RegimenFiscal");
                XmlCreador.WriteValue(regimen.Codigo);
                XmlCreador.WriteEndAttribute();
                XmlCreador.WriteEndElement(); //finaliza elemento emisor
                return true;
            }
            catch (Exception e)
            {
                error = "Ocurrio un error al generar nodo Emisor";
                _logger.EscribirError(e.ToString());
                return false;
            }
        }
        public Boolean GenerarReceptor(ref string error, Cliente cliente, UsoCFDI usoCFDI)
        {
            try
            {
                XmlCreador.WriteStartElement("cfdi:Receptor");//nodoo Receptor aqui empieza
                XmlCreador.WriteStartAttribute("Rfc");
                XmlCreador.WriteValue(DevuelveRFC(cliente.Rfc));
                XmlCreador.WriteEndAttribute();
                XmlCreador.WriteStartAttribute("Nombre");
                XmlCreador.WriteValue(cliente.Descripcion.Replace("&", "&amp;"));
                XmlCreador.WriteEndAttribute();
                XmlCreador.WriteStartAttribute("UsoCFDI");
                XmlCreador.WriteValue(usoCFDI.Codigo);
                XmlCreador.WriteEndAttribute();
                XmlCreador.WriteEndElement(); //finaliza elemento Receptor
                return true;
            }
            catch (Exception e)
            {
                error = "Ocurrio un error al generar nodo Receptor";
                _logger.EscribirError(e.ToString());
                return false;
            }
        }
        public Boolean GenerarDetalle(ref string error, ICollection<FactDetalle> detalles)
        {
            try
            {
                XmlCreador.WriteStartElement("cfdi:Conceptos"); //element de conceptos
                foreach (FactDetalle detalle in detalles)
                {
                    if ((detalle.CatSatProducto == null) ||(detalle.CatSatUnidad ==null))
                    {
                        error = "Es Necesario la clasificacion del detalle en el catalogo sat";
                        return false;
                    }
                    XmlCreador.WriteStartElement("cfdi:Concepto");//INICIO DE CONCEPTO
                    XmlCreador.WriteStartAttribute("ClaveProdServ");
                    XmlCreador.WriteValue(detalle.CatSatProducto.Codigo);
                    XmlCreador.WriteEndAttribute();
                    XmlCreador.WriteStartAttribute("NoIdentificacion");
                    XmlCreador.WriteValue(detalle.Codigo);
                    XmlCreador.WriteEndAttribute();
                    XmlCreador.WriteStartAttribute("Cantidad");
                    XmlCreador.WriteValue(detalle.Cantidad.ToString("F", CultureInfo.InvariantCulture));
                    XmlCreador.WriteEndAttribute();
                    XmlCreador.WriteStartAttribute("ClaveUnidad");
                    XmlCreador.WriteValue(detalle.CatSatUnidad.Codigo);
                    XmlCreador.WriteEndAttribute();
                    XmlCreador.WriteStartAttribute("Unidad");
                    XmlCreador.WriteValue(detalle.CatSatUnidad.Descripcion);
                    XmlCreador.WriteEndAttribute();
                    XmlCreador.WriteStartAttribute("Descripcion");
                    XmlCreador.WriteValue(detalle.Descripcion);
                    XmlCreador.WriteEndAttribute();
                    XmlCreador.WriteStartAttribute("ValorUnitario");
                    XmlCreador.WriteValue(detalle.Precio.ToString("F", CultureInfo.InvariantCulture));
                    XmlCreador.WriteEndAttribute();
                    XmlCreador.WriteStartAttribute("Descuento");
                    XmlCreador.WriteValue(detalle.Descuento.ToString("F", CultureInfo.InvariantCulture));
                    XmlCreador.WriteEndAttribute();
                    XmlCreador.WriteStartAttribute("Importe");
                    XmlCreador.WriteValue(detalle.Total.ToString("F", CultureInfo.InvariantCulture));
                    XmlCreador.WriteEndAttribute();
                    if (!String.IsNullOrEmpty(detalle.CuentaPredial))
                    {
                        XmlCreador.WriteStartElement("cfdi:CuentaPredial");//INICIO CUENTA PREDIAL
                        XmlCreador.WriteStartAttribute("Numero");
                        XmlCreador.WriteValue(detalle.CuentaPredial);
                        XmlCreador.WriteEndAttribute();
                        XmlCreador.WriteEndElement();//FIN DE CUENTA PREDIAL
                    }

                    if (detalle.Iva > 0 || detalle.Ieps > 0 || detalle.RetIsrDinero > 0 || detalle.RetIvaDinero > 0)
                    {
                        XmlCreador.WriteStartElement("cfdi:Impuestos"); //INICIO DE IMPUESTOS
                        if (detalle.Iva > 0 || detalle.Ieps > 0)
                        {
                            XmlCreador.WriteStartElement("cfdi:Traslados"); //INICIO DE TRASLADOS
                                                                            //if (detalle.Iva == -1)
                                                                            //{
                                                                            //    XmlCreador.WriteStartElement("cfdi:Traslado");//INICIO DE TRASLADO
                                                                            //    XmlCreador.WriteStartAttribute("Base");
                                                                            //    XmlCreador.WriteValue(detalle.TotalNeto.ToString("F", CultureInfo.InvariantCulture));
                                                                            //    XmlCreador.WriteEndAttribute();
                                                                            //    XmlCreador.WriteStartAttribute("Impuesto");
                                                                            //    XmlCreador.WriteValue("002");
                                                                            //    XmlCreador.WriteEndAttribute();
                                                                            //    XmlCreador.WriteStartAttribute("TipoFactor");
                                                                            //    XmlCreador.WriteValue("Exento");
                                                                            //    XmlCreador.WriteEndAttribute();
                                                                            //    XmlCreador.WriteEndElement();//FIN D
                                                                            //}
                                                                            //if (detalle.Iva == 0)
                                                                            //{
                                                                            //    XmlCreador.WriteStartElement("cfdi:Traslado");//INICIO DE TRASLADO
                                                                            //    XmlCreador.WriteStartAttribute("Base");
                                                                            //    XmlCreador.WriteValue(detalle.TotalNeto.ToString("F", CultureInfo.InvariantCulture));
                                                                            //    XmlCreador.WriteEndAttribute();
                                                                            //    XmlCreador.WriteStartAttribute("Impuesto");
                                                                            //    XmlCreador.WriteValue("002");
                                                                            //    XmlCreador.WriteEndAttribute();
                                                                            //    XmlCreador.WriteStartAttribute("TipoFactor");
                                                                            //    XmlCreador.WriteValue("Tasa");
                                                                            //    XmlCreador.WriteEndAttribute();
                                                                            //    XmlCreador.WriteStartAttribute("TasaOCuota");
                                                                            //    XmlCreador.WriteValue(detalle.TasaIva.ToString("N6", CultureInfo.InvariantCulture));
                                                                            //    XmlCreador.WriteEndAttribute();
                                                                            //    XmlCreador.WriteStartAttribute("Importe");
                                                                            //    XmlCreador.WriteValue(detalle.Iva.ToString("F", CultureInfo.InvariantCulture));
                                                                            //    XmlCreador.WriteEndAttribute();
                                                                            //    XmlCreador.WriteEndElement();//FIN D
                                                                            //}
                            if (detalle.Iva > 0)
                            {
                                XmlCreador.WriteStartElement("cfdi:Traslado");//INICIO DE TRASLADO
                                XmlCreador.WriteStartAttribute("Base");
                                XmlCreador.WriteValue(detalle.TotalNeto.ToString("F", CultureInfo.InvariantCulture));
                                XmlCreador.WriteEndAttribute();
                                XmlCreador.WriteStartAttribute("Impuesto");
                                XmlCreador.WriteValue("002");
                                XmlCreador.WriteEndAttribute();
                                XmlCreador.WriteStartAttribute("TipoFactor");
                                XmlCreador.WriteValue("Tasa");
                                XmlCreador.WriteEndAttribute();
                                XmlCreador.WriteStartAttribute("TasaOCuota");
                                XmlCreador.WriteValue(detalle.TasaIva.ToString("N6", CultureInfo.InvariantCulture));
                                XmlCreador.WriteEndAttribute();
                                XmlCreador.WriteStartAttribute("Importe");
                                XmlCreador.WriteValue(detalle.Iva.ToString("F", CultureInfo.InvariantCulture));
                                XmlCreador.WriteEndAttribute();
                                XmlCreador.WriteEndElement();//FIN D
                            }
                            if (detalle.Ieps > 0)
                            {
                                XmlCreador.WriteStartElement("cfdi:Traslado");//INICIO DE TRASLADO
                                XmlCreador.WriteStartAttribute("Base");
                                XmlCreador.WriteValue(detalle.TotalNeto.ToString("F", CultureInfo.InvariantCulture));
                                XmlCreador.WriteEndAttribute();
                                XmlCreador.WriteStartAttribute("Impuesto");
                                XmlCreador.WriteValue("003");
                                XmlCreador.WriteEndAttribute();
                                XmlCreador.WriteStartAttribute("TipoFactor");
                                XmlCreador.WriteValue("Tasa");
                                XmlCreador.WriteEndAttribute();
                                XmlCreador.WriteStartAttribute("TasaOCuota");
                                XmlCreador.WriteValue(detalle.TasaIeps.ToString("N6", CultureInfo.InvariantCulture));
                                XmlCreador.WriteEndAttribute();
                                XmlCreador.WriteStartAttribute("Importe");
                                XmlCreador.WriteValue(detalle.Ieps.ToString("F", CultureInfo.InvariantCulture));
                                XmlCreador.WriteEndAttribute();
                                XmlCreador.WriteEndElement();
                            }
                            XmlCreador.WriteEndElement();//FIN TRASLADOS
                        }
                        if (detalle.RetIsrDinero > 0 || detalle.RetIvaDinero > 0)
                        {
                            XmlCreador.WriteStartElement("cfdi:Retenciones");//RETENCIONES
                            if (detalle.RetIsrDinero > 0)
                            {
                                XmlCreador.WriteStartElement("cfdi:Retencion");//INICIO DE TRASLADO
                                XmlCreador.WriteStartAttribute("Base");
                                XmlCreador.WriteValue(detalle.TotalNeto.ToString("F", CultureInfo.InvariantCulture));
                                XmlCreador.WriteEndAttribute();
                                XmlCreador.WriteStartAttribute("Impuesto");
                                XmlCreador.WriteValue("001");
                                XmlCreador.WriteEndAttribute();
                                XmlCreador.WriteStartAttribute("TipoFactor");
                                XmlCreador.WriteValue("Tasa");
                                XmlCreador.WriteEndAttribute();
                                XmlCreador.WriteStartAttribute("TasaOCuota");
                                XmlCreador.WriteValue(detalle.TasaRetIsr.ToString("N6"));
                                XmlCreador.WriteEndAttribute();
                                XmlCreador.WriteStartAttribute("Importe");
                                XmlCreador.WriteValue(detalle.RetIsrDinero.ToString("F", CultureInfo.InvariantCulture));
                                XmlCreador.WriteEndAttribute();
                                XmlCreador.WriteEndElement();//FIN D
                            }
                            if (detalle.RetIvaDinero > 0)
                            {
                                XmlCreador.WriteStartElement("cfdi:Retencion");//INICIO DE TRASLADO
                                XmlCreador.WriteStartAttribute("Base");
                                XmlCreador.WriteValue(detalle.TotalNeto.ToString("F", CultureInfo.InvariantCulture));
                                XmlCreador.WriteEndAttribute();
                                XmlCreador.WriteStartAttribute("Impuesto");
                                XmlCreador.WriteValue("002");
                                XmlCreador.WriteEndAttribute();
                                XmlCreador.WriteStartAttribute("TipoFactor");
                                XmlCreador.WriteValue("Tasa");
                                XmlCreador.WriteEndAttribute();
                                XmlCreador.WriteStartAttribute("TasaOCuota");
                                XmlCreador.WriteValue(detalle.TasaRetIva.ToString("N6", CultureInfo.InvariantCulture));
                                XmlCreador.WriteEndAttribute();
                                XmlCreador.WriteStartAttribute("Importe");
                                XmlCreador.WriteValue(detalle.RetIvaDinero.ToString("F", CultureInfo.InvariantCulture));
                                XmlCreador.WriteEndAttribute();
                                XmlCreador.WriteEndElement();//FIN D
                            }
                            XmlCreador.WriteEndElement();// FIN RETENCIONES
                        }
                        XmlCreador.WriteEndElement();//IMPUESTOS
                    }
                    if (!String.IsNullOrEmpty(detalle.TipoComplemento))
                    {
                        //var ietu = new dynamic(detalle.Complemento);
                        dynamic ietuDinamic = detalle.Complemento;
                        Ietu ietu = ietuDinamic.ToObject<Ietu>();
                        XmlCreador.WriteStartElement("cfdi:ComplementoConcepto");
                        XmlCreador.WriteStartElement("iedu:instEducativas");
                        XmlCreador.WriteStartAttribute("Version");
                        XmlCreador.WriteValue("1.0");
                        XmlCreador.WriteEndAttribute();
                        XmlCreador.WriteStartAttribute("nombreAlumno");
                        XmlCreador.WriteValue(ietu.NombreAlumno);
                        XmlCreador.WriteEndAttribute();
                        XmlCreador.WriteStartAttribute("CURP");
                        XmlCreador.WriteValue(ietu.CURP);
                        XmlCreador.WriteEndAttribute();
                        XmlCreador.WriteStartAttribute("nivelEducativo");
                        XmlCreador.WriteValue(ietu.NivelEducativo);
                        XmlCreador.WriteEndAttribute();
                        XmlCreador.WriteStartAttribute("autRVOE");
                        XmlCreador.WriteValue(ietu.AutRVOE);
                        XmlCreador.WriteEndAttribute();
                        XmlCreador.WriteEndElement();
                        XmlCreador.WriteEndElement();
                    }
                    XmlCreador.WriteEndElement();//FIN DE CONCEPTO
                }
                XmlCreador.WriteEndElement();
                return true;//FIN DE CONCEPTOS
            }
            catch (Exception e)
            {
                error = "Ocurrio un error al generar nodo detalles";
                _logger.EscribirError(e.ToString());
                return false;
            }
        }
        public Boolean GenerarImpuestos(ref string error, Modelo.Factura factura)
        {
            try
            {
                var todosIeps = factura.Detalles.Where(d => d.TasaIeps > 0)
           .GroupBy(d => d.TasaIeps)
           .Select(cl => new
           {
               nombre = cl.First().TasaIeps,
               Suma = cl.Sum(c => c.Ieps).ToString("F"),
           }).ToList();
                var totalImpuestosRetenidos = factura.Detalles.Sum(d => d.RetIsrDinero + d.RetIvaDinero);
                var totalImpuestosTrasladados = factura.Detalles.Sum(d => d.Iva + d.Ieps);

                XmlCreador.WriteStartElement("cfdi:Impuestos"); //element de IMPUESTOS
                XmlCreador.WriteStartAttribute("TotalImpuestosTrasladados");
                XmlCreador.WriteValue(totalImpuestosTrasladados.ToString("F", CultureInfo.InvariantCulture));
                XmlCreador.WriteEndAttribute();
                XmlCreador.WriteStartAttribute("TotalImpuestosRetenidos");
                XmlCreador.WriteValue(totalImpuestosRetenidos.ToString("F", CultureInfo.InvariantCulture));
                XmlCreador.WriteEndAttribute();
                var sumRetISR = factura.Detalles.Sum(d => d.RetIsrDinero);
                var sumRetIva = factura.Detalles.Sum(d => d.RetIvaDinero);
                if (sumRetISR > 0 || sumRetIva > 0)
                {
                    XmlCreador.WriteStartElement("cfdi:Retenciones"); //cfdi:Retenciones
                    if (sumRetISR > 0)
                    {
                        XmlCreador.WriteStartElement("cfdi:Retencion"); //cfdi:Retencion IVA
                        XmlCreador.WriteStartAttribute("Impuesto");
                        XmlCreador.WriteValue("001");
                        XmlCreador.WriteEndAttribute();
                        XmlCreador.WriteStartAttribute("Importe");
                        XmlCreador.WriteValue(sumRetISR.ToString("F", CultureInfo.InvariantCulture));
                        XmlCreador.WriteEndElement();
                    }
                    if (sumRetIva > 0)
                    {
                        XmlCreador.WriteStartElement("cfdi:Retencion"); //cfdi:Retencion IVA
                        XmlCreador.WriteStartAttribute("Impuesto");
                        XmlCreador.WriteValue("002");
                        XmlCreador.WriteEndAttribute();
                        XmlCreador.WriteStartAttribute("Importe");
                        XmlCreador.WriteValue(sumRetIva.ToString("F", CultureInfo.InvariantCulture));
                        XmlCreador.WriteEndElement();
                    }
                    XmlCreador.WriteEndElement();//ELEMENT DE retenciones
                }
                var ivaGlobal = factura.Detalles.Sum(d => d.Iva);
                if (ivaGlobal > 0 || todosIeps.Count > 0)
                {
                    XmlCreador.WriteStartElement("cfdi:Traslados");
                    if (ivaGlobal > 0)
                    {
                        XmlCreador.WriteStartElement("cfdi:Traslado");
                        XmlCreador.WriteStartAttribute("Impuesto");
                        XmlCreador.WriteValue("002");
                        XmlCreador.WriteEndAttribute();
                        XmlCreador.WriteStartAttribute("TasaOCuota");
                        XmlCreador.WriteValue("0.160000");
                        XmlCreador.WriteEndAttribute();
                        XmlCreador.WriteStartAttribute("TipoFactor");
                        XmlCreador.WriteValue("Tasa");
                        XmlCreador.WriteEndAttribute();
                        XmlCreador.WriteStartAttribute("Importe");
                        XmlCreador.WriteValue(ivaGlobal.ToString("F", CultureInfo.InvariantCulture));
                        XmlCreador.WriteEndAttribute();
                        XmlCreador.WriteEndElement();
                    }
                    //if (ivaGlobal == 0)
                    //{
                    //    XmlCreador.WriteStartElement("cfdi:Traslado");
                    //    XmlCreador.WriteStartAttribute("Impuesto");
                    //    XmlCreador.WriteValue("002");
                    //    XmlCreador.WriteEndAttribute();
                    //    XmlCreador.WriteStartAttribute("TasaOCuota");
                    //    XmlCreador.WriteValue("0.000000");
                    //    XmlCreador.WriteEndAttribute();
                    //    XmlCreador.WriteStartAttribute("TipoFactor");
                    //    XmlCreador.WriteValue("Tasa");
                    //    XmlCreador.WriteEndAttribute();
                    //    XmlCreador.WriteStartAttribute("Importe");
                    //    XmlCreador.WriteValue(ivaGlobal.ToString("F", CultureInfo.InvariantCulture));
                    //    XmlCreador.WriteEndAttribute();
                    //    XmlCreador.WriteEndElement();
                    //}
                    //if (ivaGlobal == -1)
                    //{
                    //    XmlCreador.WriteStartElement("cfdi:Traslado");
                    //    XmlCreador.WriteStartAttribute("Impuesto");
                    //    XmlCreador.WriteValue("002");
                    //    XmlCreador.WriteEndAttribute();
                    //    XmlCreador.WriteStartAttribute("TasaOCuota");
                    //    XmlCreador.WriteValue("0.000000");
                    //    XmlCreador.WriteEndAttribute();
                    //    XmlCreador.WriteStartAttribute("TipoFactor");
                    //    XmlCreador.WriteValue("Tasa");
                    //    XmlCreador.WriteEndAttribute();
                    //    XmlCreador.WriteStartAttribute("Importe");
                    //    XmlCreador.WriteValue(ivaGlobal.ToString("F", CultureInfo.InvariantCulture));
                    //    XmlCreador.WriteEndAttribute();
                    //    XmlCreador.WriteEndElement();
                    //}
                    foreach (var ieps in todosIeps)
                    {
                        XmlCreador.WriteStartElement("cfdi:Traslado");
                        XmlCreador.WriteStartAttribute("Impuesto");
                        XmlCreador.WriteValue("003");
                        XmlCreador.WriteEndAttribute();
                        XmlCreador.WriteStartAttribute("TasaOCuota");
                        XmlCreador.WriteValue(ieps.nombre.ToString("N6", CultureInfo.InvariantCulture));
                        XmlCreador.WriteEndAttribute();
                        XmlCreador.WriteStartAttribute("TipoFactor");
                        XmlCreador.WriteValue("Tasa");
                        XmlCreador.WriteEndAttribute();
                        XmlCreador.WriteStartAttribute("Importe");
                        XmlCreador.WriteValue(decimal.Parse(ieps.Suma).ToString("F", CultureInfo.InvariantCulture));
                        XmlCreador.WriteEndAttribute();
                        XmlCreador.WriteEndElement();
                    }
                    XmlCreador.WriteEndElement();//ELEMENT DE Traslados
                }
                XmlCreador.WriteEndElement();//ELEMENT DE IMPUESTOS
                return true;
            }
            catch (Exception e)
            {
                _logger.EscribirError(e.ToString());
                error = "Ocurrio un error al generar nodo Impuestos";
                GenerarFin();
                return false;
            }

        }
        public void GenerarFin()
        {
            try
            {
                XmlCreador.WriteEndDocument();
                XmlCreador.Flush();
                XmlCreador.Close();
            }
            catch (Exception e)
            {
                _logger.EscribirError(e.ToString());
            }
        }
        public Boolean GenerarXML(ref string error, string ruta, Modelo.Factura factura, ICollection<DetalleTicket> productos = null)
        {
            if (productos == null)
            {
                if (!GenerarEncabezado(ref error, ruta, factura))
                    return false;
                if (!GenerarEmisor(ref error, factura.Sucursal, factura.Regimen))
                    return false;
                if (!GenerarReceptor(ref error, factura.Cliente, factura.UsoCFDI))
                    return false;
                if (!GenerarDetalle(ref error, factura.Detalles))
                    return false;
                if (!GenerarImpuestos(ref error, factura))
                    return false;

            }
            else
            {

                //GenerarEncabezadoGlobal(ref error, ruta, factura);
                //GenerarEmisor(ref error, factura);
                //GenerarReceptor(ref error, factura);
                //generarDetalle(productos);
                //GenerarImpuestosGlobal(ref error, factura, productos);
            }
            if (factura.Complementos.Count > 0)
            {
                try
                {
                    Ine ine = JsonConvert.DeserializeObject<Ine>(factura.Complementos.ElementAt(0).Detalle);
                    GenerarComplementoIne(ref error, ine);
                }
                catch
                {
                }
            }
            GenerarFin();
            return true;
        }

        public void GenerarComplementoIne(ref string error, Ine ine)
        {
            try
            {
                XmlCreador.WriteStartElement("cfdi:Complemento");
                // x.WriteStartElement("cfdi:Complemento"); //element de conceptos
                XmlCreador.WriteStartElement("ine:INE");
                XmlCreador.WriteStartAttribute("xmlns:xsi");
                XmlCreador.WriteValue("http://www.sat.gob.mx/ine");
                XmlCreador.WriteEndAttribute();
                XmlCreador.WriteStartAttribute("Version");
                XmlCreador.WriteValue("1.1");
                XmlCreador.WriteEndAttribute();
                XmlCreador.WriteStartAttribute("TipoProceso");
                XmlCreador.WriteValue(ine.TipoProceso);
                XmlCreador.WriteEndAttribute();
                if (!ine.TipoProceso.Equals("PreCampaña") && (!ine.TipoProceso.Equals("Campaña")))
                {
                    XmlCreador.WriteStartAttribute("TipoComite");
                    XmlCreador.WriteValue(ine.TipoComite);
                    XmlCreador.WriteEndAttribute();
                }
                XmlCreador.WriteStartElement("ine:Entidad");
                XmlCreador.WriteStartAttribute("ClaveEntidad");
                XmlCreador.WriteValue(ine.ClaveEntidad);
                XmlCreador.WriteEndAttribute();
                XmlCreador.WriteStartAttribute("Ambito");
                XmlCreador.WriteValue(ine.Ambito);
                XmlCreador.WriteEndAttribute();
                XmlCreador.WriteStartElement("ine:Contabilidad");
                XmlCreador.WriteStartAttribute("IdContabilidad");
                XmlCreador.WriteValue(ine.ContabilidadId);
                XmlCreador.WriteEndAttribute();
                XmlCreador.WriteEndElement();
                XmlCreador.WriteEndElement();
                XmlCreador.WriteEndElement();
                XmlCreador.WriteEndElement();
            }
            catch (Exception e)
            {
                _logger.EscribirError(e.ToString());
            }

        }
        public Boolean GeneraCFDI(ref string error, Modelo.Factura factura, string rutaGuardadoCFDI, string rutaCertificado, string rutaKey, string rutaXSLT, string rutaCarpetaTemp, ICollection<DetalleTicket> productos = null)
        {
            try
            {
                ServicioCertificado _servicioCertificado = new ServicioCertificado(_logger);
                factura.NoCertificadoEmisor = _servicioCertificado.GetNumeroCertificado(ref error, rutaCertificado);
                if (String.IsNullOrEmpty(factura.NoCertificadoEmisor))
                    return false;
                this.Certificado = _servicioCertificado.GetCertificado(rutaCarpetaTemp, rutaCertificado, factura.Sucursal.RutaCer);
                if (String.IsNullOrEmpty(this.Certificado))
                    return false;
                if(!GenerarXML(ref error, rutaGuardadoCFDI, factura))
                {
                    this.GenerarFin();
                    return false;
                }
                factura.CadenaOriginal = _servicioCertificado.GetCadenaOriginal(ref error, rutaGuardadoCFDI, rutaXSLT);
                if (String.IsNullOrEmpty(factura.CadenaOriginal))
                    return false;
                this.SelloDigital = _servicioCertificado.GetSelloDigital(ref error, rutaKey, factura.Sucursal.ClavePrivada, factura.CadenaOriginal);
                if (String.IsNullOrEmpty(this.SelloDigital))
                    return false;
                if (!GenerarXML(ref error, rutaGuardadoCFDI, factura))
                {
                    this.GenerarFin();
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                GenerarFin();
                _logger.EscribirError(e.ToString());
                return false;
            }
        }

        public Boolean Timbrar(ref string errores, ref XmlDocument xml, string rutaXmlSinTImbrar, string rutaXmlTimbrado)
        {
            try
            {
                xml.Load(rutaXmlSinTImbrar);
                StampSOAP timbrado = new StampSOAP();
                stamp timb = new stamp();
                timb.xml = RegresaEN64(xml.OuterXml);
                timb.username = Resources.UsuarioFinkokP;
                timb.password = Resources.ContraFinkok; ;
                stampResponse respuestaTimbrado = timbrado.stamp(timb);
                if ((respuestaTimbrado.stampResult.Incidencias.Length == 0) || (respuestaTimbrado.stampResult.Incidencias[0].CodigoError.Contains("307")))
                {
                    xml.LoadXml(respuestaTimbrado.stampResult.xml);
                    xml.Save(rutaXmlTimbrado);
                }
                else
                    errores = respuestaTimbrado.stampResult.Incidencias[0].MensajeIncidencia;
            }
            catch (Exception e)
            {
                errores += e.Message;
            }
            return string.IsNullOrEmpty(errores) ? true : false;
        }

        public Boolean RecuperarXML( string rfc, string folioFiscal, string rutaGuardado)
        {
            try
            {
                Utilerias.get_xml _datosXml = new Utilerias.get_xml();
                _datosXml.invoice_type = "I";
                _datosXml.username = Resources.UsuarioFinkokP;
                _datosXml.password = Resources.ContraFinkok;
                _datosXml.taxpayer_id = rfc;
                _datosXml.uuid = folioFiscal;
                Utilerias.get_xmlResponse respuesta = new Utilerias.get_xmlResponse();
                Utilerias.UtilitiesSOAP utilitiesSOAP = new Utilerias.UtilitiesSOAP();
                respuesta = utilitiesSOAP.get_xml(_datosXml);
                if (respuesta.get_xmlResult.xml != null)
                {
                    XmlDocument _xml = new XmlDocument();
                    _xml.LoadXml(respuesta.get_xmlResult.xml);
                    _xml.Save(rutaGuardado);
                }
                return true;
            }
            catch(Exception e)
            {
                _logger.EscribirError(e.ToString());
                return false;
            }
        }

        public Boolean GeneraCBB(ref string error, string rfcEmisor, string rfcReceptor, string folioFiscal, decimal total, string rutaGuardado)
        {
            String dataCBB = string.Format("?re={0}&amp;rr={1}&amp;tt={2:N6}&amp;id={3}", rfcEmisor, rfcReceptor, total, folioFiscal);
            Bitmap imagen = null;
            try
            {
                ThoughtWorks.QRCode.Codec.QRCodeEncoder generarCodigoQR = new QRCodeEncoder();
                generarCodigoQR.QRCodeEncodeMode = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ENCODE_MODE.BYTE;
                generarCodigoQR.QRCodeScale = 4;
                generarCodigoQR.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
                generarCodigoQR.QRCodeVersion = 0;
                generarCodigoQR.QRCodeBackgroundColor = Color.Black;
                generarCodigoQR.QRCodeBackgroundColor = Color.White;
                imagen = generarCodigoQR.Encode(dataCBB);
                imagen.Save(rutaGuardado);
                return true;
            }
            catch (Exception e)
            {
                this._logger.EscribirError(e.ToString());
                error = "Error al generar codigo QR de factura";
                return false;
            }
        }

        public Boolean Cancelar(ref string error, ref Modelo.Factura factura, string rutaCertificado, string rutaKey)
        {
            var _rutaCertificadoPem = String.Format("{0}{1}", rutaCertificado, ".pem");
            var _rutaKeyPem = String.Format("{0}{1}", rutaKey, ".pem");
            ServicioCertificado _servicioCertificado = new ServicioCertificado(this._logger);
            _servicioCertificado.CreaArchivoExtPEM(rutaCertificado, _rutaCertificadoPem);
            _servicioCertificado.CreaArchivoKeyPEM(rutaKey, _rutaKeyPem, factura.Sucursal.ClavePrivada);
            cancelResponse _respuestaCancelacion = null;
            try
            {
                CancelSOAP cancela = new CancelSOAP();
                cancel can = new cancel();
                can.username = Resources.UsuarioFinkokP;
                can.password = Resources.ContraFinkok;
                can.taxpayer_id = factura.Sucursal.Rfc;
                can.cer = RegresaEN64(File.ReadAllText(_rutaCertificadoPem));
                can.key = RegresaEN64(File.ReadAllText(_rutaKeyPem));
                UUIDS nim = new UUIDS();
                string[] uuidString = new string[] { factura.FolioFiscal };
                nim.uuids = uuidString.ToArray();
                can.UUIDS = nim;
                _respuestaCancelacion = cancela.cancel(can);
                if ((_respuestaCancelacion != null) && (_respuestaCancelacion.cancelResult != null))
                {
                    factura.AcuseCancelacion = _respuestaCancelacion.cancelResult.Acuse;
                    factura.FechaCancelacion = DateTime.Now;
                    factura.Estatus = "Cancelada";
                }
                return true;
            }
            catch(Exception e)
            {
                this._logger.EscribirError(e.ToString());
                return false ;
            }
        }

        public string DevuelveRFC(string rfc)
        {
            string _rfc = string.Empty;
            _rfc = rfc.Replace('-', ' ');
            _rfc = rfc.Trim().TrimEnd().TrimStart();
            _rfc = _rfc.Replace("&", "&amp;");
            return _rfc;
        }

        public static byte[] RegresaEN64(string xml)
        {
            byte[] resp = null;
            try
            {
                resp = Encoding.UTF8.GetBytes(xml);
                string s = Convert.ToBase64String(resp);
                resp = Convert.FromBase64String(s);
            }
            catch (Exception)
            {
            }
            return resp;
        }
    }
}
