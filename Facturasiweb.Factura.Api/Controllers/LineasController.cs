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
    [RoutePrefix("api/lineas"), CustomAuthorize]
    public class LineasController : BaseController
    {
        ServicioLinea _servicio = null;
        public LineasController(Context contexto) : base(contexto)
        {
            _servicio = new ServicioLinea(this._logger, contexto);
        }
        [Route(""),HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(_servicio.Get(Util.USUARIO.Id));
        }
        [Route("{id}"), HttpGet]
        public IHttpActionResult GetId(int id)
        {
            return Ok(_servicio.GetId(id));
        }
        [Route(""),HttpPost]
        public IHttpActionResult Post(Linea linea)
        {
            linea.UsuarioId = Util.USUARIO.Id;
            if (_servicio.Post(ref _error, linea))
                return Ok("linea Guardado Correctamente");
            else
                return BadRequest(_error);
        }
        [Route(""), HttpPost]
        public IHttpActionResult PostSublinea(Linea linea)
        {
            linea.UsuarioId = Util.USUARIO.Id;
            if (_servicio.Post(ref _error, linea))
                return Ok("Sub-linea Guardado Correctamente");
            else
                return BadRequest(_error);
        }
        [Route(""),HttpPut]
        public IHttpActionResult Put(Linea linea)
        {
            if (_servicio.Put(ref _error, linea))
                return Ok("linea Guardado Correctamente");
            else
                return BadRequest(_error);
        }
        [HttpDelete]
        public IHttpActionResult Delete(Linea linea)
        {
            if (_servicio.Put(ref _error, linea))
                return Ok("linea Eliminado Correctamente");
            else
                return BadRequest(_error);
        }
    }
}
