using Facturasiweb.Factura.Modelo;
using Facturasiweb.Factura.Modelo.Complementos;
using System;
using System.Data.Entity;
using Facturasiweb.Factura.Modelo.PuntoVenta;

namespace Facturasiweb.Factura.DAO
{
    public interface IContext
    {
        DbSet<CatalogoDireccion> CatalogoDirecciones { get; set; }
        DbSet<CatSatProducto> CatSatProductos { get; set; }
        DbSet<CatSatUnidad> CatSatUnidades { get; set; }
        DbSet<Cliente> Clientes { get; set; }
        DbSet<Caja> Cajas { get; set; }
        DbSet<ComplementoPago> ComplementoPagos { get; set; }
        DbSet<Complemento> Complementos { get; set; }
        DbSet<Cotizacion> Cotizaciones { get; set; }
        DbSet<Configuracion> Configuraciones { get; set; }
        DbSet<Equipo> Equipos { get; set; }
        void disabled();
        void enabled();
        DbSet<Modelo.Factura> Facturas { get; set; }
        DbSet<FormaPago> FormaPagos { get; set; }
        DbSet<Formato> Formatos { get; set; }
        DbSet<MetodoPago> MetodoPagos { get; set; }
        DbSet<Pais> Paises { get; set; }
        DbSet<Producto> Productos { get; set; }
        DbSet<Regimen> Regimenes { get; set; }
        DbSet<Serie> Series { get; set; }
        DbSet<Servicio> Servicios { get; set; }
        DbSet<Sucursal> Sucursales { get; set; }
        DbSet<SucursalRegimen> SucursalRegimenes { get; set; }
        DbSet<Turno> Turnos { get; set; }
        DbSet<Modelo.PuntoVenta.PuntoVenta> PuntoVentas { get; set; }
        DbSet<PuntoVentaDetalle> PuntoVentaDetalles { get; set; }
        DbSet<TipoVenta> TipoVentas { get; set; }
        DbSet<UsoCFDI> UsoCFDIs { get; set; }
        DbSet<Usuario> Usuarios { get; set; }
    }
}
