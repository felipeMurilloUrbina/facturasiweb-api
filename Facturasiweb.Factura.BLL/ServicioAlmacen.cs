using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facturasiweb.Factura.DAO;
using Facturasiweb.Factura.LogDLL;
using Facturasiweb.Factura.Modelo;
using Facturasiweb.Factura.Modelo.DTO;

namespace Facturasiweb.Factura.BLL
{
    public class ServicioAlmacen : ServicioBase
    {
        public ServicioAlmacen(Logger logger, Context contexto) : base(logger, contexto)
        {
        }
        public ICollection<Almacen> Get(int usuarioId, int sucursalId)
        {
            try
            {
                return this._contexto.Almacenes
                    .Include(a=>a.Sucursal)
                    .Where(p => p.UsuarioId == usuarioId && p.SucursalId==sucursalId)
                    .ToList();
            }
            catch (Exception e)
            {
                this._logger.EscribirError(e.ToString());
                return null;
            }
        }
        public Almacen GetId(int almacenId)
        {
            try
            {
                return this._contexto.Almacenes
                    .Include(l => l.Sucursal)
                    .Where(p => p.Id == almacenId).FirstOrDefault();
            }
            catch (Exception e)
            {
                this._logger.EscribirError(e.ToString());
                return null;
            }
        }
        public Boolean Post(ref string error, Almacen almacen, int sucursalId, UsuarioDto usuario)
        {
            almacen.UsuarioCreadorId = usuario.Id;
            almacen.UsuarioModificadorId = usuario.Id;
            almacen.SucursalId = sucursalId;
            almacen.UsuarioId = usuario.UsuarioSistemaId;
            if (almacen == null)
            {
                error = "El Almacen no puede ser vacio.";
                return false;
            }
            if (almacen.Codigo.ToString().Length == 0)
            {
                error = "El  codigo de almacen no puede ser vacio.";
                return false;
            }
            this._contexto.Almacenes.Add(almacen);
            try
            {
                this._contexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                this._logger.EscribirError(e.ToString());
                error = "Ocurrio un error al guardar Almacen.";
                return false;
            }
        }
        public Boolean Put(ref string error, Almacen almacen)
        {
            if (almacen == null)
            {
                error = "El Almacen no puede ser vacio.";
                return false;
            }
            if (almacen.Codigo.ToString().Length == 0)
            {
                error = "El  codigo de almacen no puede ser vacio.";
                return false;
            }
            this._contexto.Entry(almacen).State = EntityState.Modified;
            try
            {
                this._contexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                this._logger.EscribirError(e.ToString());
                error = "Ocurrio un error al guardar Almacen.";
                return false;
            }
        }
    }
}
