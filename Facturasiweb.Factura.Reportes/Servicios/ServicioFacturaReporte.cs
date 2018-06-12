using CrystalDecisions.CrystalReports.Engine;
using Facturasiweb.Factura.LogDLL;
using Facturasiweb.Factura.Modelo.Complementos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.Reportes.Servicios
{
   public static class ServicioFacturaReporte
    {
        public static ReportClass GenerarPDF(ref string error, Modelo.Factura factura, Logger logger)
        {
            try
            {
                var facturaReporte = (ReportClass)Activator.CreateInstance(Type.GetType(factura.Formato.NombreFormato));
                //var facturaReporte = new Reportes.FacturaNormalLila();
                Datos.Datos datos = new Datos.Datos();
                var _colores = factura.Sucursal.ColorReporte.Split(',');
                datos.Cliente.AddClienteRow(
                    factura.Cliente.Descripcion, factura.Cliente.Rfc, factura.Cliente.Calle, factura.Cliente.CodigoPostal,
                    factura.Cliente.Ciudad, factura.Cliente.Colonia, factura.Cliente.Municipio, factura.Cliente.Estado, factura.Cliente.Pais
                    );
                datos.Sucursal.AddSucursalRow(
                    factura.Sucursal.Descripcion, factura.Sucursal.Rfc, factura.Sucursal.Calle, factura.Sucursal.CodigoPostal,
                    factura.Sucursal.Ciudad, factura.Sucursal.Colonia, factura.Sucursal.Municipio, factura.Sucursal.Estado, factura.Sucursal.Pais, factura.Sucursal.LogoBytes, factura.Regimen.Descripcion,
                    "", factura.Sucursal.Telefono, factura.Sucursal.Celular, int.Parse(_colores[0]), int.Parse(_colores[1]), int.Parse(_colores[2])
                    );
                if (factura.Complementos.Count > 0)
                {
                    try
                    {
                        Ine ine = JsonConvert.DeserializeObject<Ine>(factura.Complementos.ElementAt(0).Detalle);
                        datos.Ine.AddIneRow(ine.TipoProceso, ine.TipoComite, ine.ContabilidadId, ine.Ambito, ine.ClaveEntidad, ine.Version);
                    }
                    catch
                    {
                    }
                }
                datos.Factura.AddFacturaRow(
                    factura.Id,
                    factura.Serie, factura.Folio, factura.Tipoventa, factura.Fecha, factura.CantidadEnLetra,
                    factura.MetodoPago.Descripcion, factura.FormaPago.Descripcion, factura.Banco, factura.NoCuentaPago, factura.SubtotalE,
                    factura.SubtotalG, factura.Iva, factura.RetIva, factura.RetIsr, factura.Descuento, factura.Total, factura.SelloCfd, factura.SelloSat, factura.NoCertificadoSat,
                    factura.NoCertificadoEmisor, factura.FechaTimbrado, factura.VersionTimbrado, factura.CadenaOriginal, factura.Tipo, "A", factura.ImagenCbbBytes, factura.FolioFiscal, factura.Ieps, (bool)factura.EsCredito, factura.Observacion
                    );
                datos.RegimenFiscal.AddRegimenFiscalRow(factura.Regimen.Codigo, factura.Regimen.Descripcion);
                datos.FormaPago.AddFormaPagoRow(factura.FormaPago.Codigo, factura.FormaPago.Descripcion);
                datos.MetodoPago.AddMetodoPagoRow(factura.MetodoPago.Codigo, factura.MetodoPago.Descripcion);
                datos.UsoCFDI.AddUsoCFDIRow(factura.UsoCFDI.Codigo, factura.UsoCFDI.Descripcion);
                foreach (var det in factura.Detalles)
                {
                    datos.CatalogoSat.AddCatalogoSatRow(det.CatSatProducto.Codigo, det.CatSatProducto.Descripcion);
                    datos.FactDetalle.AddFactDetalleRow(det.CatSatProducto.Codigo, det.CuentaPredial, det.Codigo, det.Descripcion, det.CatSatUnidad.Descripcion, det.TasaIva,
                      det.TasaIeps, det.Precio, det.Cantidad, det.TasaDesc, det.Total, det.Lote, det.FechaCaducidad, det.CatSatUnidad.Codigo);
                }

                facturaReporte.SetDataSource(datos);
                return facturaReporte;
            }
            catch (Exception e)
            {
                error = e.ToString();
                logger.EscribirError(e.ToString());
                return null;
            }


        }

        public static ReportClass GenerarPDFGlogal(ref string error, Modelo.Factura factura, Logger logger)
        {
            try
            {
                var facturaReporte = (ReportClass)Activator.CreateInstance(Type.GetType(factura.Formato.NombreFormato));
                Datos.Datos datos = new Datos.Datos();
                var _colores = factura.Sucursal.ColorReporte.Split(',');
                datos.Cliente.AddClienteRow(
                    factura.Cliente.Descripcion, factura.Cliente.Rfc, factura.Cliente.Calle, factura.Cliente.CodigoPostal,
                    factura.Cliente.Ciudad, factura.Cliente.Colonia, factura.Cliente.Municipio, factura.Cliente.Estado, factura.Cliente.Pais
                    );
                datos.Sucursal.AddSucursalRow(
                    factura.Sucursal.Descripcion, factura.Sucursal.Rfc, factura.Sucursal.Calle, factura.Sucursal.CodigoPostal,
                    factura.Sucursal.Ciudad, factura.Sucursal.Colonia, factura.Sucursal.Municipio, factura.Sucursal.Estado, factura.Sucursal.Pais, factura.Sucursal.LogoBytes, factura.Regimen.Descripcion,
                    "", factura.Sucursal.Telefono, factura.Sucursal.Celular, int.Parse(_colores[0]), int.Parse(_colores[1]), int.Parse(_colores[2])
                    );

                if (factura.Complementos.Count > 0)
                {
                    try
                    {
                        Ine ine = JsonConvert.DeserializeObject<Ine>(factura.Complementos.ElementAt(0).Detalle);
                        datos.Ine.AddIneRow(ine.TipoProceso, ine.TipoComite, ine.ContabilidadId, ine.Ambito, ine.ClaveEntidad, ine.Version);
                    }
                    catch
                    {
                    }

                }
                datos.Factura.AddFacturaRow(
                    factura.Id,
                    factura.Serie, factura.Folio, factura.Tipoventa, factura.Fecha, factura.CantidadEnLetra,
                    factura.MetodoPago.Descripcion, factura.FormaPago.Descripcion, factura.Banco, factura.NoCuentaPago, factura.SubtotalE,
                    (Decimal)factura.SubtotalGlobal, (Decimal)factura.IvaGlobal, factura.RetIva, factura.RetIsr, factura.Descuento, (Decimal)factura.TotalGlobal, factura.SelloCfd, factura.SelloSat, factura.NoCertificadoSat,
                    factura.NoCertificadoEmisor, factura.FechaTimbrado, factura.VersionTimbrado, factura.CadenaOriginal, factura.Tipo, "A", factura.ImagenCbbBytes, factura.FolioFiscal, (decimal)factura.IepsGlobal, (bool)factura.EsCredito, factura.Observacion
                    );
                datos.RegimenFiscal.AddRegimenFiscalRow(factura.Regimen.Codigo, factura.Regimen.Descripcion);
                datos.FormaPago.AddFormaPagoRow(factura.FormaPago.Codigo, factura.FormaPago.Descripcion);
                datos.MetodoPago.AddMetodoPagoRow(factura.MetodoPago.Codigo, factura.MetodoPago.Descripcion);
                datos.UsoCFDI.AddUsoCFDIRow(factura.UsoCFDI.Codigo, factura.UsoCFDI.Descripcion);
                foreach (var det in factura.Detalles)
                {
                    datos.CatalogoSat.AddCatalogoSatRow(det.CatSatProducto.Codigo, det.CatSatProducto.Descripcion);
                    datos.FactDetalle.AddFactDetalleRow(det.CatSatProducto.Codigo, det.CuentaPredial, det.Codigo, det.Descripcion, det.CatSatUnidad.Descripcion, det.TasaIva,
                        det.TasaIeps, det.Precio, det.Cantidad, det.TasaDesc, det.Total, det.Lote, det.FechaCaducidad, det.CatSatUnidad.Codigo);
                }

                facturaReporte.SetDataSource(datos);
                return facturaReporte;
            }
            catch (Exception e)
            {
                error = e.ToString();
                logger.EscribirError(e.ToString());
                return null;
            }
        }
    }
}
