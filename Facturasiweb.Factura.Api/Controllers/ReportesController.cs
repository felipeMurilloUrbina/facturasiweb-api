using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Facturasiweb.Factura.Api.Servicios;
using Facturasiweb.Factura.BLL;
using Facturasiweb.Factura.BLL.Modelos;
using Facturasiweb.Factura.DAO;

namespace Facturasiweb.Factura.Api.Controllers
{
    [RoutePrefix("api/reportes"), CustomAuthorize]
    public class ReportesController : BaseController
    {
        ServicioReportes _servicio = null;
        public ReportesController(Context contexto) : base(contexto)
        {
            this._servicio = new ServicioReportes(this._logger, _contexto);
        }
        [HttpGet]
        public IHttpActionResult Get()
        {
            var _sucursal = Request.Headers.GetValues("sucursal").FirstOrDefault();
            //var _usuario = Util.DecryptToken(Request.Headers);
            if (String.IsNullOrWhiteSpace(_sucursal))
                return BadRequest("Se requierela sucursal");
            return Ok(_servicio.GetContador(Util.USUARIO.UsuarioSistemaId, int.Parse(_sucursal)));
        }
        [Route("facturas"), HttpGet]
        public IHttpActionResult GetFacturas()
        {
            var _sucursal = Request.Headers.GetValues("sucursal").FirstOrDefault();
            if (String.IsNullOrWhiteSpace(_sucursal))
                return BadRequest("Se requierela sucursal");
            return Ok(_servicio.GetFacturasxMes(int.Parse(_sucursal)));
        }
        [CustomAuthorize, Route("facturas/listado"), HttpPost]
        public IHttpActionResult GetReporteFacturas([FromBody]OpcionesReporte opciones)
        {
            var _sucursal = Request.Headers.GetValues("sucursal").FirstOrDefault();
            return Ok(_servicio.GetListado(opciones, int.Parse(_sucursal)));
        }
    }
}
