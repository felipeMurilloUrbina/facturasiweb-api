using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facturasiweb.Factura.BLL.Modelos;
using Facturasiweb.Factura.DAO;
using Facturasiweb.Factura.LogDLL;
using System.Data.Entity;
namespace Facturasiweb.Factura.BLL
{
    public class ServicioReportes : ServicioBase
    {
        public ServicioReportes(Logger logger, Context contexto) : base(logger, contexto)
        {
        }
        public Contador GetContador(int usuarioId, int sucursalId)
        {
            int sucursales = _contexto.Sucursales.Where(s => s.UsuarioId == usuarioId).Count();
            int productos = _contexto.Productos.Where(s => s.UsuarioId == usuarioId).Count();
            int clientes = _contexto.Clientes.Where(s => s.UsuarioId == usuarioId).Count();
            int facturas = _contexto.Facturas.Where(f => f.SucursalId == sucursalId).Count();
            return new Contador() {
                Sucursales=sucursales,
                Productos= productos,
                Clientes= clientes,
                Facturas= facturas
            };
        }
        public Object GetFacturasxMes(int sucursalId)
        {
            this._contexto.disabled();
            var facturas = this._contexto.Facturas.Include(f=>f.Cliente).Include(f=>f.Detalles).Where(f => f.SucursalId == sucursalId).ToList();
            var facturasxMes = facturas.GroupBy(x => new
            {
                Month = x.Fecha.Month,
                Year = x.Fecha.Year
            }).Select(f => new { label = String.Format("{0}.{1}", f.Key.Month, f.Key.Year), Cantidad = f.Count() });
            var facturasxCliente = facturas.Where(f => f.Cliente != null).GroupBy(x => new
            {
                cliente = x.Cliente.Descripcion
            }).Select(f => new { label = String.Format("{0}", f.Key.cliente), Cantidad = f.Sum(fac => fac.Total) / 1000 }).OrderByDescending(f => f.Cantidad).Take(10);
            this._contexto.enabled();
            return (new { facturasxMes, facturasxCliente });
        }
        public ICollection<Modelo.Factura> GetListado(OpcionesReporte opciones, int sucursalId)
        {

            this._contexto.disabled();
            opciones.Inicio = opciones.Inicio.AddDays(-1);
            opciones.Fin = opciones.Fin.AddDays(1);
            var _facturas = this._contexto.Facturas.Include(f => f.Cliente).Include(f=>f.MetodoPago).Include(f=>f.FormaPago).Include(f => f.Detalles).Where(f => f.SucursalId == sucursalId && f.Fecha > opciones.Inicio && f.Fecha < opciones.Fin).ToList();

            List<Modelo.Factura> _listaEnvio = new List<Modelo.Factura>();
            if ((opciones.Clientes != null))
            {
                if (opciones.Clientes.Count > 0)
                {
                    _facturas.ForEach(f =>
                    {
                        if (opciones.Clientes.Where(c => c.Id == f.ClienteId).FirstOrDefault() != null)
                            _listaEnvio.Add(f);
                    });
                }
            }
            else
            {
                _listaEnvio = _facturas;
            }
            _facturas = _listaEnvio;
            if (opciones.MetodosPago != null)
            {
                if (opciones.MetodosPago.Count > 0)
                {
                   _listaEnvio = new List<Modelo.Factura>();
                    _facturas.ForEach(f =>
                    {
                        if (opciones.MetodosPago.Where(c => c.Id == f.MetodopagoId).FirstOrDefault() != null)
                            _listaEnvio.Add(f);
                    });
                }
            }
            return _listaEnvio.OrderBy(f => f.ClienteId).ToList();
        }
    }
}
