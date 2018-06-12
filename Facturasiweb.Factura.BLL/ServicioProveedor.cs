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
    public class ServicioProveedor : ServicioBase
    {
        public ServicioProveedor(Logger logger, Context contexto) : base(logger, contexto)
        {
        }
        public ICollection<Proveedor> Get(int usuarioId)
        {
            try
            {
                return this._contexto.Proveedores.Where(s => s.UsuarioId == usuarioId).ToList();
            }
            catch (Exception e )
            {
                return null;
            }
        }
        public Proveedor GetId(int proveedorId)
        {
            return this._contexto.Proveedores.Find(proveedorId);
        }

        public Boolean Post(ref string error, Proveedor proveedor, UsuarioDto usuario)
        {
            proveedor.UsuarioCreadorId = usuario.Id;
            proveedor.UsuarioModificadorId = usuario.Id;
            proveedor.UsuarioId = usuario.UsuarioSistemaId;
            this._contexto.Proveedores.Add(proveedor);
            try
            {
                this._contexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                this._logger.EscribirError(e.ToString());
                error = "Ocurrio un problema al querer guardar";
                return false;
            }
        }

        public Boolean Put(ref string error, Proveedor proveedor, UsuarioDto usuario)
        {
            proveedor.UsuarioModificadorId = usuario.Id;
            proveedor.UsuarioId = usuario.UsuarioSistemaId;
            this._contexto.Entry(proveedor).State = EntityState.Modified;
            try
            {
                this._contexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                this._logger.EscribirError(e.ToString());
                error = "Ocurrio un problema al querer guardar";
                return false;
            }
        }

        private string validarProveedor(Proveedor proveedor)
        {
            if (proveedor == null)
                return "No puede estar vacio el proveedor.";
            if (String.IsNullOrEmpty(proveedor.Rfc))
                return "El proveedor debe tener RFC.";
            if (String.IsNullOrEmpty(proveedor.Descripcion))
                return "El proveedor debe tener razón social.";
            return "";
        }

    }
}
