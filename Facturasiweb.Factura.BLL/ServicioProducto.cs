using System;
using System.Collections.Generic;
using System.Linq;
using Facturasiweb.Factura.DAO;
using Facturasiweb.Factura.LogDLL;
using Facturasiweb.Factura.Modelo;
using System.Data.Entity;
using Facturasiweb.Factura.Modelo.DTO;

namespace Facturasiweb.Factura.BLL
{
    public class ServicioProducto : ServicioBase
    {
        public ServicioProducto(Logger logger, Context contexto) : base(logger, contexto)
        {
        }
        public ICollection<Producto> Get(int usuarioId)
        {
            try
            {
                return this._contexto.Productos.Include(p => p.CatalogoSat).Where(p => p.UsuarioId == usuarioId).ToList();
            }
            catch (Exception e)
            {
                this._logger.EscribirError("", e.ToString());
                return null;
            }
        }
        public Producto GetId(int productoId)
        {
            try
            {
                return this._contexto.Productos.Include(p => p.CatalogoSat).Include(p => p.CatSatUnidad).Where(p => p.Id == productoId).FirstOrDefault();
            }
            catch (Exception e)
            {
                this._logger.EscribirError("", e.ToString());
                return null;
            }
        }
        public Boolean Post(ref string error, Producto producto, UsuarioDto usuario)
        {
            if (producto == null)
            {
                error = "El producto no puede ser vacio.";
                return false;
            }
            if (this._contexto.Productos.Where(u => u.Codigo.Equals(producto.Codigo) && u.UsuarioId==usuario.Id).FirstOrDefault()!=null)
            {
                error = "El  codigo de ya se utiliza en otro producto.";
                return false;
            }
            //if (producto.CatalogoId == 0 || producto.UnidadId == 0)
            //{
            //    error = "Es necesario el codigo del sat / unidad.";
            //    return false;
            //}
            producto.UsuarioCreadorId = usuario.Id;
            producto.UsuarioModificadorId = usuario.Id;
            producto.UsuarioId = usuario.UsuarioSistemaId;

            this._contexto.Productos.Add(producto);
            try
            {
                this._contexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                this._logger.EscribirError("", e.ToString());
                error = "Ocurrio un error al guardar producto.";
                return false;
            }
        }
        public Boolean Put(ref string error, Producto producto)
        {
            if (producto == null)
            {
                error = "El producto no puede ser vacio.";
                return false;
            }
            if (this._contexto.Productos.Where(u => u.Codigo.Equals(producto.Codigo)).Count() > 1)
            {
                error = "El  codigo de ya se utiliza en otro producto.";
                return false;
            }
            //if (producto.CatalogoId == 0 || producto.UnidadId == 0)
            //{
            //    error = "Es necesario el codigo del sat / unidad.";
            //    return false;
            //}
            this._contexto.Entry(producto).State = EntityState.Modified;
            try
            {
                this._contexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                this._logger.EscribirError("", e.ToString());
                error = "Ocurrio un error al guardar producto.";
                return false;
            }
        }
    }
}
