using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.DynamicFilters;
using Facturasiweb.Factura.Modelo;
using Facturasiweb.Factura.Modelo.Complementos;
using Facturasiweb.Factura.Modelo.Interfaces;
using Facturasiweb.Factura.Modelo.PuntoVenta;

namespace Facturasiweb.Factura.DAO
{
    public class Context : DbContext, IContext
    {

        public Context()
            : base("name=GlobalEntities")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Entity<Producto>().Property(p => p.Precio).HasPrecision(18, 6);
            modelBuilder.Entity<Producto>().Property(p => p.Costo).HasPrecision(18, 6);
            modelBuilder.Entity<Producto>().Property(p => p.TasaRetIva).HasPrecision(18, 6);
            modelBuilder.Entity<Producto>().Property(p => p.TasaRetIsr).HasPrecision(18, 6);
            modelBuilder.Entity<Producto>().Property(p => p.TasaIeps).HasPrecision(18, 6);
            modelBuilder.Entity<Producto>().Property(p => p.TasaIva).HasPrecision(18, 6);
            modelBuilder.Entity<FactDetalle>().Property(p => p.Precio).HasPrecision(18, 6);
            modelBuilder.Entity<FactDetalle>().Property(p => p.Cantidad).HasPrecision(18, 6);
            modelBuilder.Entity<FactDetalle>().Property(p => p.TasaRetIva).HasPrecision(18, 6);
            modelBuilder.Entity<FactDetalle>().Property(p => p.TasaRetIsr).HasPrecision(18, 6);
            modelBuilder.Entity<FactDetalle>().Property(p => p.TasaIeps).HasPrecision(18, 6);
            modelBuilder.Entity<FactDetalle>().Property(p => p.TasaIva).HasPrecision(18, 6);
            AgregarFiltros(ref modelBuilder);
        }
        private void AgregarFiltros(ref DbModelBuilder modelBuilder)
        {
            //modelBuilder.Filter
            modelBuilder.Filter("BorradoLogico", (IBorradoLogico d) => d.Activo, true);
        }

        public void disabled()
        {
            this.DisableFilter("BorradoLogico");
        }
        public void enabled()
        {
            this.EnableFilter("BorradoLogico");
        }
        public virtual DbSet<Almacen> Almacenes { get; set; }
        public virtual DbSet<CatSatProducto> CatSatProductos { get; set; }
        public virtual DbSet<CatalogoDireccion> CatalogoDirecciones { get; set; }
        public virtual DbSet<CatSatUnidad> CatSatUnidades { get; set; }
        public virtual DbSet<Caja> Cajas { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Configuracion> Configuraciones { get; set; }
        public virtual DbSet<Complemento> Complementos { get; set; }
        public virtual DbSet<ComplementoPago> ComplementoPagos { get; set; }
        public DbSet<Cotizacion> Cotizaciones { get; set; }
        public virtual DbSet<Equipo> Equipos { get; set; }
        public virtual DbSet<Modelo.Factura> Facturas { get; set; }
        public virtual DbSet<Formato> Formatos { get; set; }
        public virtual DbSet<FormaPago> FormaPagos { get; set; }
        public virtual DbSet<Linea> Lineas { get; set; }
        public virtual DbSet<MetodoPago> MetodoPagos { get; set; }
        public virtual DbSet<Proveedor> Proveedores { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<PuntoVenta> PuntoVentas { get; set; }
        public virtual DbSet<Regimen> Regimenes { get; set; }
        public virtual DbSet<TipoMovimiento> TiposMovimiento { get; set; }
        public virtual DbSet<Servicio> Servicios { get; set; }
        public virtual DbSet<Serie> Series { get; set; }
        public virtual DbSet<Sucursal> Sucursales { get; set; }
        public virtual DbSet<SucursalRegimen> SucursalRegimenes { get; set; }
        public virtual DbSet<Pais> Paises { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<UsuarioSistema> UsuariosSistema { get; set; }
        public virtual DbSet<PuntoVentaDetalle> PuntoVentaDetalles { get; set; }
        public virtual DbSet<TipoVenta> TipoVentas { get; set; }
        public virtual DbSet<Turno> Turnos { get; set; }
        public virtual DbSet<TurnoCaja> TurnoCajas { get; set; }
        public virtual DbSet<UsoCFDI> UsoCFDIs { get; set; }
    }
}
