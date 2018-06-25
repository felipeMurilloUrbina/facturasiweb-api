using Facturasiweb.Factura.DAO;
using Facturasiweb.Factura.LogDLL;
using Facturasiweb.Factura.Modelo;
using Facturasiweb.Factura.Modelo.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.BLL
{
    public class ServicioCliente: ServicioBase 
    {
        public ServicioCliente(Context contexto, Logger logger): base(logger, contexto)
        {
        }
        public ICollection<Cliente> Get(int usuarioId)
        {
            return this._contexto.Clientes.Where(s => s.UsuarioId == usuarioId).ToList();
        }
        public Cliente GetId(int clienteId)
        {
            try
            {
                return this._contexto.Clientes.Include(c=>c.Direcciones).Where(c=>c.Id==clienteId).FirstOrDefault();
            }
            catch (Exception e)
            {

                throw;
            }
        }
        public Boolean Post(ref string error, Cliente cliente, UsuarioDto usuario)
        {
            try
            {
                cliente.UsuarioCreadorId = usuario.Id;
                cliente.UsuarioModificadorId = usuario.Id;
                cliente.UsuarioId = usuario.UsuarioSistemaId;
                this._contexto.Clientes.Add(cliente);
                this._contexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                this._logger.EscribirError(e.ToString());
                error = "Ocurrio un problema al guardar";
                return false;
            }
        }
        public Boolean Put(ref string error, Cliente cliente, UsuarioDto usuario)
        {
            cliente.UsuarioModificadorId = usuario.Id;
            cliente.UsuarioId = usuario.UsuarioSistemaId;
            this._contexto.Entry(cliente).State = EntityState.Modified;
            try
               {
<<<<<<< HEAD
                this._contexto.Database.ExecuteSqlCommand($"DELETE FROM DIRECCIONES WHERE CLIENTEID={cliente.Id}");
=======
                this._contexto.Database.ExecuteSqlCommandAsync($"DELETE FROM DIRECCIONES WHERE CLIENTEID={cliente.Id}");
>>>>>>> da1751b45aec14c3b969efd9be0fbee0c078b634
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
        private string validarCliente(Cliente cliente)
        {
            if (cliente == null)
                return "No puede estar vacio el cliente.";
            if (String.IsNullOrEmpty(cliente.Rfc))
                return "El cliente debe tener RFC.";
            if (String.IsNullOrEmpty(cliente.Descripcion))
                return "El cliente debe tener razón social.";
            if (cliente.Direcciones.Count() == 0)
                return "El cliente debe de tener al menos una sucursal.";
            return "";
        }
    }
}