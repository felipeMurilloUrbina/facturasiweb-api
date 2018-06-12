using Facturasiweb.Factura.DAO;
using Facturasiweb.Factura.LogDLL;
using Facturasiweb.Factura.Modelo;
using Facturasiweb.Factura.Modelo.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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
            return this._contexto.Clientes.Find(clienteId);
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
            if(String.IsNullOrEmpty(cliente.Descripcion))
                return "El cliente debe tener razón social.";
            return "";
        }
    }
}