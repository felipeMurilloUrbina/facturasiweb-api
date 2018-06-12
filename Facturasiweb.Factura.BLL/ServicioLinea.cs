using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facturasiweb.Factura.DAO;
using Facturasiweb.Factura.LogDLL;
using Facturasiweb.Factura.Modelo;

namespace Facturasiweb.Factura.BLL
{
    public class ServicioLinea : ServicioBase
    {
        public ServicioLinea(Logger logger, Context contexto) : base(logger, contexto)
        {
        }
        public ICollection<Linea> Get(int usuarioId)
        {
            try
            {
                return this._contexto.Lineas
                    .Include(l => l.SubLinea)
                    .Where(p => p.UsuarioId == usuarioId)
                    .ToList();
            }
            catch (Exception e)
            {
                this._logger.EscribirError(e.ToString());
                return null;
            }
        }
        public Linea GetId(int LineaId)
        {
            try
            {
                return this._contexto.Lineas
                    .Include(l =>l.SubLinea )
                    .Where(p => p.Id == LineaId).FirstOrDefault();
            }
            catch (Exception e)
            {
                this._logger.EscribirError(e.ToString());
                return null;
            }
        }
        public Boolean Post(ref string error, Linea linea)
        {
            if (linea == null)
            {
                error = "El linea no puede ser vacio.";
                return false;
            }
            if (linea.Codigo.ToString().Length == 0)
            {
                error = "El  codigo de almacen no puede ser vacio.";
                return false;
            }
            this._contexto.Lineas.Add(linea);
            try
            {
                this._contexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                this._logger.EscribirError( e.ToString());
                error = "Ocurrio un error al guardar linea.";
                return false;
            }
        }
        public Boolean Put(ref string error, Linea linea)
        {
            if (linea == null)
            {
                error = "El linea no puede ser vacio.";
                return false;
            }
            if (linea.Codigo.ToString().Length == 0)
            {
                error = "El  codigo de almacen no puede ser vacio.";
                return false;
            }
            this._contexto.Entry(linea).State = EntityState.Modified;
            try
            {
                this._contexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                this._logger.EscribirError(e.ToString());
                error = "Ocurrio un error al guardar linea.";
                return false;
            }
        }
    }
}
