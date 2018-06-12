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
    [RoutePrefix("api/almacenes"), CustomAuthorize]
    public class AlmacenesController : BaseController
    {
        ServicioAlmacen _servicio = null;
        public AlmacenesController(Context contexto) : base(contexto)
        {
            this._servicio = new ServicioAlmacen(this._logger, this._contexto);
        }
        [HttpGet]
        public IHttpActionResult Get()
        {
            var _sucursal = Request.Headers.GetValues("sucursal").FirstOrDefault();
            if (_sucursal == null)
                return BadRequest("Seleccione una sucursal primero.");
            return Ok(_servicio.Get(Util.USUARIO.UsuarioSistemaId, int.Parse(_sucursal)));
        }
        [Route("{id}"), HttpGet]
        public IHttpActionResult GetId(int id)
        {
            return Ok(_servicio.GetId(id));
        }
        [HttpPost]
        public IHttpActionResult Post(Almacen Almacen)
        {
            var _sucursal = Request.Headers.GetValues("sucursal").FirstOrDefault();
            if (_sucursal == null)
                return BadRequest("Seleccione una sucursal primero.");
            if (_servicio.Post(ref _error, Almacen, int.Parse(_sucursal), Util.USUARIO))
                return Ok("Almacen Guardado Correctamente");
            else
                return BadRequest(_error);
        }
        [HttpPut]
        public IHttpActionResult Put(Almacen Almacen)
        {
            if (_servicio.Put(ref _error, Almacen))
                return Ok("Almacen Guardado Correctamente");
            else
                return BadRequest(_error);
        }
        [HttpDelete]
        public IHttpActionResult Delete(Almacen Almacen)
        {
            if (_servicio.Put(ref _error, Almacen))
                return Ok("Almacen Eliminado Correctamente");
            else
                return BadRequest(_error);
        }
    }
}
