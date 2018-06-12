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
    [CustomAuthorize]
    public class ProveedoresController : BaseController
    {
        ServicioProveedor _servicio = null;
        public ProveedoresController(Context contexto) : base(contexto)
        {
            this._servicio = new ServicioProveedor(this._logger, this._contexto);
        }
        public IHttpActionResult Get()
        {
            //var usuario = Util.DecryptToken(Request.Headers);
            return Ok(this._servicio.Get(Util.USUARIO.UsuarioSistemaId));
        }
        /// <summary>
        /// REGRESA UN OBJECTO Proveedor CON UN ID ESPECIFICO
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
        /// GUARDA UN Proveedor DE UN USUARIO LOGEADO
        /// </summary>
        /// <param name="Proveedor">OBJECTO DE TIPO DE Proveedor</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Post(Proveedor proveedor)
        {
            proveedor.UsuarioId = Util.USUARIO.Id;
            if (_servicio.Post(ref _error, proveedor, Util.USUARIO))
                return Ok("Proveedor Guardada Correctamente");
            else
                return BadRequest(_error);
        }
        /// <summary>
        /// ACTUALIZA EL OBJECTO DE TIPO DE CLIENTE
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult Put(Proveedor proveedor)
        {
            //var usuario = Util.DecryptToken(Request.Headers);
            if (_servicio.Put(ref _error, proveedor, Util.USUARIO))
                return Ok("Proveedor Guardada Correctamente");
            else
                return BadRequest(_error);
        }
        /// <summary>
        /// ELIMINA EL OBJECTO Proveedor 
        /// </summary>
        /// <param name="Proveedor"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult Delete(Proveedor proveedor)
        {
            if (_servicio.Put(ref _error, proveedor, Util.USUARIO))
                return Ok("Proveedor Eliminado Correctamente");
            else
                return BadRequest(_error);
        }
    }
}
