using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Facturasiweb.Factura.Api.Models;
using Facturasiweb.Factura.Api.Servicios;
using Facturasiweb.Factura.BLL;
using Facturasiweb.Factura.DAO;
using Facturasiweb.Factura.Modelo;

namespace Facturasiweb.Factura.Api.Controllers
{
    [RoutePrefix("api/sucursales"), CustomAuthorize]
    public class SucursalesController : BaseController
    {
        private ServicioSucursal _servicio = null;
        public SucursalesController(Context contexto) : base(contexto)
        {
            this._servicio = new ServicioSucursal(this._contexto, this._logger);
        }

        public IHttpActionResult Get()
        {
            return Ok(this._servicio.Get(Util.USUARIO.UsuarioSistemaId));
        }
        [Route("{id}"), HttpGet]
        public IHttpActionResult GetId(int id)
        {
            return Ok(_servicio.GetId(id));
        }
        [Route("{id}/archivo"), HttpPost]
        public IHttpActionResult PostFile(int id)
        {

            var _httpRequest = HttpContext.Current.Request;
            if (_httpRequest.Files.Count == 0)
                return BadRequest("No hay archivos que subir.");
            var _sucursal = this._servicio.GetId(id);
            if (_sucursal == null)
                return BadRequest("No Existe la sucursal.");
            if (_servicio.UploadFile(ref _error, HttpContext.Current, _sucursal))
                return Ok("Archivo subido correctamente");
            else
                return BadRequest(_error);
        }
        [HttpPost]
        public IHttpActionResult Post(Sucursal sucursal)
        {
            sucursal.UsuarioId = Util.USUARIO.UsuarioSistemaId;
            if (_servicio.Post(ref _error, sucursal, Util.USUARIO))
                return Ok("Sucursal Guardada Correctamente");
            else
                return BadRequest(_error);
        }
        [Route("template-correo"), HttpPost]
        public IHttpActionResult PostAsunto(AsuntoMensaje asuntoMensaje)
        {
            var _sucursal = Request.Headers.GetValues("sucursal").FirstOrDefault();
            return Ok();
        }
        [HttpPut]
        public IHttpActionResult Put(Sucursal sucursal)
        {
            if (_servicio.Put(ref _error, sucursal))
                return Ok("Sucursal Guardada Correctamente");
            else
                return BadRequest(_error);
        }
        [HttpDelete]
        public IHttpActionResult Delete(Sucursal sucursal)
        {
            if (_servicio.Put(ref _error, sucursal))
                return Ok("Sucursal Eliminado Correctamente");
            else
                return BadRequest(_error);
        }
    }
}
