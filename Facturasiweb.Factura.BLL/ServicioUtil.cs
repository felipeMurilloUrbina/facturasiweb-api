using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facturasiweb.Factura.BLL.Modelos;
using Facturasiweb.Factura.DAO;
using Facturasiweb.Factura.LogDLL;
using Facturasiweb.Factura.Modelo;
using System.Data.Entity;
namespace Facturasiweb.Factura.BLL
{
    public class ServicioUtil : ServicioBase
    {
        public ServicioUtil(Logger logger, Context contexto) : base(logger, contexto)
        {
        }
        public void AgregarUsuario()
        {
            var _sucursales = this._contexto.Sucursales.Include(s=>s.UsuarioCreador).Include(s => s.Usuario).ToList();
            foreach (var _sucursal in _sucursales)
            {
                if (_sucursal.UsuarioCreador != null)
                {
                    _sucursal.Asunto = _sucursal.UsuarioCreador.Asunto;
                    _sucursal.Mensaje = _sucursal.UsuarioCreador.Mensaje;
                    this._contexto.Entry(_sucursal).State = EntityState.Modified;
                    this._contexto.SaveChanges();
                }
            }
        }
        public ICollection<CatSatUnidad> GetUnidades(string busqueda)
        {
            var _unidades = _contexto.CatSatUnidades.Where(p => p.Descripcion.ToLower().Equals(busqueda.ToLower())).OrderBy(u => u.Descripcion).ToList();
            if (_unidades.Count == 0)
                _unidades = _contexto.CatSatUnidades.Where(p => p.Descripcion.ToLower().Contains(busqueda.ToLower())).Take(20).OrderBy(u => u.Descripcion).ToList();
            if (_unidades.Count == 0)
                _unidades = _contexto.CatSatUnidades.Where(p => p.Codigo.ToLower().Contains(busqueda.ToLower())).Take(20).OrderBy(u => u.Codigo).ToList();
            return _unidades;
        }

        public ICollection<CatSatProducto> GetCatalogoSat(string busqueda)
        {
            var _productos = _contexto.CatSatProductos.Where(p => p.Descripcion.ToLower().Equals(busqueda.ToLower())).OrderBy(u => u.Descripcion).ToList();
            if (_productos.Count == 0)
                _productos = _contexto.CatSatProductos.Where(p => p.Descripcion.ToLower().Contains(busqueda.ToLower())).Take(20).OrderBy(u => u.Descripcion).ToList();
            if (_productos.Count == 0)
                _productos = _contexto.CatSatProductos.Where(p => p.Codigo.ToLower().Contains(busqueda.ToLower())).Take(20).OrderBy(u => u.Codigo).ToList();
            return _productos;
        }
        public Catalogo GetCatalogos()
        {
            var _regimenes = _contexto.Regimenes.OrderBy(r => r.Descripcion).ToList();
            var _paises = _contexto.Paises.ToList();
            var _formatos = _contexto.Formatos.ToList();
            return new Catalogo()
            {
                Regimenes = _regimenes,
                Paises = _paises,
                Formatos = _formatos
            };
        }
        public ICollection<String> GetMunicipios(string estado)
        {
            return _contexto.CatalogoDirecciones.Where(c => c.Estado == estado).Select(c => c.Municipio).Distinct().ToList();
        }
        public Object GetLocalidades(string estado, string municipio)
        {
            return _contexto.CatalogoDirecciones.Where(c => c.Estado == estado && c.Municipio == municipio).Select(c => new { c.Localidad, c.CodigoPostal }).ToList();
        }
        public ICollection<MetodoPago> GetMetodosPago()
        {
            return _contexto.MetodoPagos.ToList();
        }

        public ICollection<FormaPago> GetFormasPago()
        {
            return _contexto.FormaPagos.ToList();
        }
        public ICollection<UsoCFDI> GetUsoCFDIs()
        {
            return _contexto.UsoCFDIs.ToList();
        }
    }
}
