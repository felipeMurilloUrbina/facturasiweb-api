using AutoMapper;
using Facturasiweb.Factura.BLL.Modelos;
using Facturasiweb.Factura.DAO;
using Facturasiweb.Factura.LogDLL;
using Facturasiweb.Factura.Modelo;
using Facturasiweb.Factura.Modelo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Facturasiweb.Factura.BLL
{
    public class ServicioUsuario : ServicioBase
    {
        public ServicioUsuario(Logger logger, Context contexto ) : base(logger, contexto)
        {
        }

        public ICollection<Usuario> Get(UsuarioDto usuario)
        {
            this._contexto.disabled();
            List<Usuario> _listaUsuarios = null;
            if(usuario.Tipo.Equals("Admin"))
            {
                _listaUsuarios = _contexto.Usuarios.ToList();
            } else {
                _listaUsuarios = _contexto.Usuarios.Where(u => u.UsuarioSistemaId==usuario.UsuarioSistemaId).ToList();
            }
            this._contexto.enabled();
            return _listaUsuarios;
        }

        public LoginUsuario Login(ref string error, Usuario usuario)
        {

            var isValid = usuario == null ? false : true;
            if ((!isValid) && (string.IsNullOrEmpty(usuario.Contra)))
            {
                error = "Datos son incorrectos";
                return null;
            }
            var _contra = ServicioEncriptacion.Encrypt(usuario.Contra);

            var usuarioLogger = this._contexto.Usuarios.Include(u => u.UsuarioSistema.Sucursales).Include(u=>u.UsuarioSistema).Where(u => u.NombreUsuario.ToLower().Equals(usuario.NombreUsuario.ToLower()) && u.Contra == _contra).FirstOrDefault();
            if (usuarioLogger != null)
            {
                List<Sucursal> sucursales = null;
                if ((usuarioLogger.SucursalId != null) && (usuarioLogger.SucursalId > 0))
                    sucursales = this._contexto.Sucursales.Where(s => s.UsuarioId == usuarioLogger.SucursalId).ToList();
                else
                    sucursales = usuarioLogger.UsuarioSistema.Sucursales.ToList();
                return new LoginUsuario() { Token = ServicioEncriptacion.GeneraToken(usuarioLogger), Sucursales = sucursales };
            }
            else
            {
                error = "Datos son incorrectos";
                return null;
            }
        }

        public Boolean Post(ref string error, Usuario usuario, UsuarioDto usuarioCreador)
        {
            if (usuario == null)
            {
                error = "El usuario no puede ser vacio.";
                return false;
            }
            if(this._contexto.Usuarios.Where(u=> u.NombreUsuario.Equals(usuario.NombreUsuario)).Count()>0)
            {
                error = "El nombre de usuario ya se utiliza.";
                return false;
            }
            usuario.UsuarioCreadorId = usuarioCreador.Id;
            usuario.UsuarioModificadorId = usuarioCreador.Id;
            usuario.UsuarioSistemaId = usuarioCreador.UsuarioSistemaId;
            usuario.Avatar = "default.png";
            usuario.Contra = ServicioEncriptacion.Encrypt(usuario.Contra);
            usuario.Activo = true;
            usuario.FechaCreacion = DateTime.Now;
            usuario.Plantilla = 1;
            usuario.Tipo = "Cliente";
            this._contexto.Usuarios.Add(usuario);
            try
            {
                this._contexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                this._logger.EscribirError("", e.ToString());
                error = "Ocurrio un error al guardar usuario.";
                return false;
            }
        }

        public Boolean Put(ref string error, Usuario usuario)
        {
            if (usuario == null)
            {
                error = "El usuario no puede ser vacio.";
                return false;
            }
            this._contexto.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
            this._contexto.Entry(usuario).Property(p => p.TimbresComprados).IsModified = false;
            this._contexto.Entry(usuario).Property(p => p.TimbresUsados).IsModified = false;
            this._contexto.Entry(usuario).Property(p => p.FechaCreacion).IsModified = false;
            try
            {
                this._contexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                this._logger.EscribirError("", e.ToString());
                error = "Ocurrio un error al guardar usuario.";
                return false;
            }
        }
        
    }
}
