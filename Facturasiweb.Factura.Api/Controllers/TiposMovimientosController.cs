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
    [RoutePrefix("api/tipos-movimientos"), CustomAuthorize]
    public class TiposMovimientosController : BaseController
    {
        ServicioTipoMovimiento _servicio = null;
        public TiposMovimientosController(Context contexto) : base(contexto)
        {
            this._servicio = new ServicioTipoMovimiento(this._logger, this._contexto);
        }
        [Route(""), HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(_servicio.Get(Util.USUARIO.UsuarioSistemaId));
        }
        [Route(""),HttpPost]
        public IHttpActionResult Post(TipoMovimiento tipoMovimiento)
        {
            if (_servicio.Post(ref _error,Util.USUARIO, tipoMovimiento))
                return Ok("Tipo de movimiento Guardado Correctamente");
            else
                return BadRequest(_error);
        }
        [Route(""),HttpPut]
        public IHttpActionResult Put(TipoMovimiento tipoMovimiento)
        {
            if (_servicio.Put(ref _error, tipoMovimiento))
                return Ok("Tipo de movimiento Guardado Correctamente");
            else
                return BadRequest(_error);
        }
        [Route(""), HttpDelete]
        public IHttpActionResult Delete(TipoMovimiento tipoMovimiento)
        {
            if (_servicio.Put(ref _error, tipoMovimiento))
                return Ok("Tipo de movimiento Eliminado Correctamente");
            else
                return BadRequest(_error);
        }
    }
}
