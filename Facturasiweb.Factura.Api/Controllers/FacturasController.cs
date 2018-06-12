using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using Facturasiweb.Factura.Api.Servicios;
using Facturasiweb.Factura.BLL;
using Facturasiweb.Factura.DAO;

namespace Facturasiweb.Factura.Api.Controllers
{
    [RoutePrefix("api/facturas")]
    public class FacturasController : BaseController
    {
        ServicioFactura _servicio = null;
        public FacturasController(Context contexto) : base(contexto)
        {
            this._servicio = new ServicioFactura(this._logger, this._contexto);
        }
        [CustomAuthorize, Route(""), HttpGet]
        public IHttpActionResult Get()
        {
            var _sucursal = Request.Headers.GetValues("sucursal").FirstOrDefault();
            return Ok(_servicio.Get(Util.USUARIO, int.Parse(_sucursal)));
        }
        [CustomAuthorize, Route("nueva")]
        public IHttpActionResult GetNueva()
        {
            var _sucursal = Request.Headers.GetValues("sucursal").FirstOrDefault();
            return Ok(_servicio.GetNueva(int.Parse(_sucursal)));

        }
        [Route("{clienteid}")]
        public IHttpActionResult Get(int clienteId)
        {
            var _sucursal = Request.Headers.GetValues("sucursal").FirstOrDefault();
            return Ok(_servicio.Get(clienteId, int.Parse(_sucursal)));
        }
        [Route(""), HttpPost, CustomAuthorize]
        public IHttpActionResult Post(Modelo.Factura factura)
        {
            if (_servicio.Post(ref _error, factura, Util.USUARIO, HttpContext.Current))
                return Ok("Factura Guardada Correctamente");
            else
                return BadRequest(_error);
        }
        [AllowAnonymous, Route("{id}/pdf"), HttpGet]
        public IHttpActionResult GetFilePDF(int id)
        {
            string _nombre = "";
            Modelo.Factura _factura = new Modelo.Factura();
            var _archivo = _servicio.GetFile(ref _nombre, ref _factura, 2, id, HttpContext.Current);
            if (_archivo == null)
                return BadRequest("No se encuentra el archivo.");

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(_archivo);
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = _nombre

            };
            return ResponseMessage(response);
        }
        [AllowAnonymous, Route("{id}/xml"), HttpGet]
        public IHttpActionResult GetFileXML(int id)
        {
            string _nombre = "";
            Modelo.Factura _factura = new Modelo.Factura();
            var _archivo = _servicio.GetFile(ref _nombre, ref _factura, 1, id, HttpContext.Current);
            if (_archivo == null)
                return BadRequest("No se encuentra el archivo.");
            else
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(_archivo);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = _nombre
                };
                return ResponseMessage(response);
            }
        }
        [CustomAuthorize, Route("{id}/reenviar"), HttpPost]
        public IHttpActionResult Reenviar(int id)
        {
            //var _usuario = Util.DecryptToken(Request.Headers);
            if (_servicio.Reenviar(ref _error, Util.USUARIO, id, HttpContext.Current))
            {
                return Ok("Factura reenviada correctamente.");
            }
            else
                return BadRequest(_error);
        }
        [CustomAuthorize, Route("{facturaId}/cancelar"), HttpPost]
        public IHttpActionResult Cancelar(int facturaId)
        {
            if (_servicio.Cancelar(ref _error, facturaId, Util.USUARIO, HttpContext.Current))
                return Ok("Factura cancelada correctamente");
            else
                return BadRequest(_error);
        }
    }
}