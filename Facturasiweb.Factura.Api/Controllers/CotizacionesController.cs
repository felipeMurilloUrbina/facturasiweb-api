using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using Facturasiweb.Factura.Api.Servicios;
using Facturasiweb.Factura.BLL;
using Facturasiweb.Factura.DAO;
using Facturasiweb.Factura.Modelo;

namespace Facturasiweb.Factura.Api.Controllers
{
    [RoutePrefix("api/cotizaciones"), CustomAuthorize]
    public class CotizacionesController : BaseController
    {
        ServicioCotizacion _servicio = null;
        public CotizacionesController(Context contexto) : base(contexto)
        {
            this._servicio = new ServicioCotizacion(this._logger, _contexto);
        }
        [HttpGet]
        public IHttpActionResult Get()
        {
            var _sucursal = Request.Headers.GetValues("sucursal").FirstOrDefault();
            return Ok(_servicio.Get(int.Parse(_sucursal)));
        }
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            return Ok(_servicio.GetId(id));
        }
        [Route("nueva"),HttpGet]
        public IHttpActionResult GetNueva()
        {
            var _sucursal = Request.Headers.GetValues("sucursal").FirstOrDefault();
            if (_sucursal == null)
                return BadRequest("Seleccione una sucursal.");
            return Ok(_servicio.GetNueva(int.Parse(_sucursal)));
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
            if (_servicio.UploadFile(ref _error, HttpContext.Current, id, Util.USUARIO))
                return Ok("Archivo subido correctamente");
            else
                return BadRequest(_error);
        }
        public IHttpActionResult Post(Cotizacion cotizacion)
        {
            try
            {
                var _sucursal = Request.Headers.GetValues("sucursal").FirstOrDefault();
                if (_sucursal == null)
                    return BadRequest("No existe sucursal.");
                cotizacion.SucursalId = int.Parse(_sucursal);
                cotizacion.UsuarioId = Util.USUARIO.Id;
                if (_servicio.Post(ref _error, cotizacion))
                    return Ok("Cotizacion guardada correctamente");
                else
                    return BadRequest(_error);
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
            
        }
        [Route("reenviar"), HttpPost]
        public IHttpActionResult Put(Cotizacion cotizacion)
        {
            return Ok();
        }
        [Route("{id}/archivo/{archivoid}"), HttpGet]
        public IHttpActionResult GetFile(int id, int archivoId)
        {
            string _nombre = "";
            var _cotizacion = new Cotizacion();
            _cotizacion.Id = id;
            var _archivo = _servicio.GetFile( ref _error, ref _nombre, ref _cotizacion, archivoId, HttpContext.Current);
            if (_archivo == null)
                return BadRequest(_error);

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(_archivo);
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName =_nombre 

            };
            return ResponseMessage(response);
        }
        [Route("{id}/pdf"), HttpGet]
        public IHttpActionResult GetPDF(int id)
        {
            string _nombre = "";
            var _cotizacion = new Cotizacion();
            _cotizacion.Id = id;
            var _archivo = _servicio.GetPDF(ref _error, ref _nombre, ref _cotizacion, HttpContext.Current);
            if (_archivo == null)
                return BadRequest(_error);

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(_archivo);
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = _nombre

            };
            return ResponseMessage(response);
        }
        public IHttpActionResult Delete()
        {
            return Ok();
        }
    }
}
