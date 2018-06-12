using Facturasiweb.Factura.Api.Servicios;
using Facturasiweb.Factura.BLL;
using Facturasiweb.Factura.DAO;
using Facturasiweb.Factura.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Facturasiweb.Factura.Api.Controllers
{
    [CustomAuthorize, RoutePrefix("api/productos")]
    public class ProductosController : BaseController
    {
        private ServicioProducto _servicio = null;
        public ProductosController(Context contexto) : base(contexto)
        {
            this._servicio = new ServicioProducto(this._logger, contexto);
        }
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(_servicio.Get(Util.USUARIO.UsuarioSistemaId));
        }
        [Route("{id}"), HttpGet]
        public IHttpActionResult GetId(int id)
        {
            return Ok(_servicio.GetId(id));
        }
        [HttpPost]
        public IHttpActionResult Post(Producto producto)
        {
            producto.UsuarioId = Util.USUARIO.Id;
            if (_servicio.Post(ref _error, producto, Util.USUARIO))
                return Ok("Producto Guardado Correctamente");
            else
                return BadRequest(_error);
        }
        [HttpPut]
        public IHttpActionResult Put(Producto producto)
        {
            if (_servicio.Put(ref _error, producto))
                return Ok("Producto Guardado Correctamente");
            else
                return BadRequest(_error);
        }
        [HttpDelete]
        public IHttpActionResult Delete(Producto producto)
        {
            if (_servicio.Put(ref _error, producto))
                return Ok("Producto Eliminado Correctamente");
            else
                return BadRequest(_error);
        }
    }
}
