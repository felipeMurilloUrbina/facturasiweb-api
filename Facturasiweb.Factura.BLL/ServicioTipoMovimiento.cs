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
    public class ServicioTipoMovimiento : ServicioBase
    {
        public ServicioTipoMovimiento(Logger logger, Context contexto) : base(logger, contexto)
        {
        }
        public ICollection<TipoMovimiento> Get(int usuarioid)
        {
            try
            {
                return this._contexto.TiposMovimiento.Where(tm => tm.UsuarioSistemaId == usuarioid).ToList();
            }
            catch (Exception e )
            {
                this._logger.EscribirError(e.ToString());
                return null;
            }
        }
        public Boolean Post(ref string error, UsuarioDto usuario, TipoMovimiento tipoMovimiento)
        {
            if (tipoMovimiento == null)
            {
                error = "El tipo de movimiento no puede ser vacio.";
                return false;
            }
            if (tipoMovimiento.Codigo.ToString().Length == 0)
            {
                error = "El codigo de tipo de movimiento  no puede ser vacio.";
                return false;
            }
            this._contexto.TiposMovimiento.Add(tipoMovimiento);
            try
            {
                this._contexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                this._logger.EscribirError(e.ToString());
                error = "Ocurrio un error al guardar tipo de movimiento.";
                return false;
            }
        }
        public Boolean Put(ref string error, TipoMovimiento tipoMovimiento)
        {
            if (tipoMovimiento == null)
            {
                error = "El tipo de movimiento no puede ser vacio.";
                return false;
            }
            if (tipoMovimiento.Codigo.ToString().Length == 0)
            {
                error = "El codigo de tipo de movimiento  no puede ser vacio.";
                return false;
            }
            this._contexto.Entry(tipoMovimiento).State = EntityState.Modified;
            try
            {
                this._contexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                this._logger.EscribirError(e.ToString());
                error = "Ocurrio un error al guardar tipo de movimiento.";
                return false;
            }
        }
    }
}
