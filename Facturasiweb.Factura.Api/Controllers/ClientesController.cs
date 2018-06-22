using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Facturasiweb.Factura.Api.Servicios;
using Facturasiweb.Factura.BLL;
using Facturasiweb.Factura.DAO;
using Facturasiweb.Factura.Modelo;

namespace Facturasiweb.Factura.Api.Controllers
{
    [RoutePrefix("api/clientes"), CustomAuthorize]
    public class ClientesController : BaseController
    {
        ServicioCliente _servicio = null;
        public ClientesController(Context contexto) : base(contexto)
        {
            this._servicio = new ServicioCliente(this._contexto, this._logger);
        }
        /// <summary>
        /// REGRESA TODOS LOS CLIENTES DEL USUARIO LOGEADO
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            //var usuario = Util.DecryptToken(Request.Headers);
            return Ok(this._servicio.Get(Util.USUARIO.UsuarioSistemaId));
        }
        /// <summary>
        /// REGRESA UN OBJECTO CLIENTE CON UN ID ESPECIFICO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id}"), HttpGet]
        public IHttpActionResult GetId(int id)
        {
            //var usuario = Util.DecryptToken(Request.Headers);
            return Ok(_servicio.GetId(id));
        }
        /// <summary>
        /// GUARDA UN CLIENTE DE UN USUARIO LOGEADO
        /// </summary>
        /// <param name="cliente">OBJECTO DE TIPO DE CLIENTE</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Post(Cliente cliente)
        {
            if (_servicio.Post(ref _error, cliente, Util.USUARIO))
                return Ok("Cliente Guardado Correctamente");
            else
                return BadRequest(_error);
        }
        /// <summary>
        /// ACTUALIZA EL OBJECTO DE TIPO DE CLIENTE
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult Put(Cliente cliente)
        {
            //var usuario = Util.DecryptToken(Request.Headers);
            if (_servicio.Put(ref _error, cliente, Util.USUARIO))
                return Ok("Cliente Guardado Correctamente");
            else
                return BadRequest(_error);
        }
        /// <summary>
        /// ELIMINA EL OBJECTO CLIENTE 
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult Delete(Cliente cliente)
        {
            if (_servicio.Put(ref _error, cliente, Util.USUARIO))
                return Ok("Cliente Eliminado Correctamente");
            else
                return BadRequest(_error);
        }
    }
}