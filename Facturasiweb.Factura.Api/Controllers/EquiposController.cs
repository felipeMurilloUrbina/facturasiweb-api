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
    public class EquiposController : BaseController
    {
        private ServicioEquipo _servicio = null;
        public EquiposController(Context contexto) : base(contexto)
        {
            this._servicio = new ServicioEquipo(this._logger, this._contexto);
        }
        public IHttpActionResult Get()
        {
            var usuario = Util.DecryptToken(Request.Headers);
            return Ok(this._servicio.Get(usuario.Id));
        }
        [Route("{id}"), HttpGet]
        public IHttpActionResult GetId(int id)
        {
            var usuario = Util.DecryptToken(Request.Headers);
            return Ok(_servicio.GetId(id));
        }
        [HttpPost]
        public IHttpActionResult Post(Equipo equipo)
        {
            string error = "";
            if (_servicio.Post(ref error, equipo, Util.USUARIO))
                return Ok("Equipo Guardada Correctamente");
            else
                return BadRequest(error);
        }
        [HttpPut]
        public IHttpActionResult Put(Equipo equipo)
        {
            string error = "";
            if (_servicio.Put(ref error, equipo, Util.USUARIO))
                return Ok("Equipo Guardada Correctamente");
            else
                return BadRequest(error);
        }
        [HttpDelete]
        public IHttpActionResult Delete(Equipo equipo)
        {
            string error = "";
            if (_servicio.Put(ref error, equipo, Util.USUARIO))
                return Ok("Equipo Eliminado Correctamente");
            else
                return BadRequest(error);
        }
    }
}
