using Facturasiweb.Factura.BLL;
using Facturasiweb.Factura.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Facturasiweb.Factura.Api.Controllers
{
    [RoutePrefix("api/util")]
    public class UtilController : BaseController
    {
        private ServicioUtil _servicio = null;
        public UtilController(Context contexto) : base(contexto)
        {
            this._servicio = new ServicioUtil(this._logger, contexto);
        }
        public IHttpActionResult Get()
        {
            _servicio.AgregarUsuario();
            return Ok();
        }
        [Route("unidades/cantidad"), HttpGet]
        public IHttpActionResult GetCantidadUnidades()
        {
            return Ok(_servicio.GetCantidadUnidades());
        }
        [Route("catalogos/cantidad"), HttpGet]
        public IHttpActionResult GetCantidadCatalogos()
        {
            return Ok(_servicio.GetCantidadCatalogo());
        }
        [Route("unidades/{busqueda}"), HttpGet]
        public IHttpActionResult GetUnidades([FromUri]string busqueda)
        {
            return Ok(_servicio.GetUnidades(busqueda));
        }
        [Route("unidades/{pagina}/{registros}"), HttpGet]
        public IHttpActionResult GetTodasUnidades(int pagina, int registros)
        {
            return Ok(_servicio.GetUnidades(pagina, registros));
        }
        [Route("catalogos/{pagina}/{registros}"), HttpGet]
        public IHttpActionResult GetTodosCatalogo(int pagina, int registros)
        {
            return Ok(_servicio.GetUnidades(pagina, registros));
        }
        [Route("catalogos/{busqueda}"), HttpGet]
        public IHttpActionResult GetCatalogos([FromUri]string busqueda)
        {
            return Ok(_servicio.GetCatalogoSat(busqueda));
        }
        [Route("catalogos"), HttpGet]
        public IHttpActionResult GetCatalogos()
        {
            return Ok(_servicio.GetCatalogos());
        }
        [Route("formas-pago"), HttpGet]
        public IHttpActionResult GetFormaPago()
        {
            return Ok(_servicio.GetFormasPago());
        }
        [Route("metodos-pago"), HttpGet]
        public IHttpActionResult GetMetodoPago()
        {
            return Ok(_servicio.GetMetodosPago());
        }
        [Route("usocfdis"), HttpGet]
        public IHttpActionResult GetUsoCFDI()
        {
            return Ok(_servicio.GetUsoCFDIs());
        }
        [Route("{estado}"), HttpGet]
        public IHttpActionResult GetEstados(string estado)
        {
            return Ok(_servicio.GetMunicipios(estado));
        }
        [Route("{estado}/{municipio}"), HttpGet]
        public IHttpActionResult GetCatalogos(string estado, string municipio)
        {
            return Ok(_servicio.GetLocalidades(estado, municipio));
        }
        //[Route("codigopostal/{codigoPostal}"), HttpGet]
        //public IHttpActionResult GetCatalogo(string codigoPostal)
        //{
        //    var localidad = db.CatalogoDirecciones.Where(c => c.CodigoPostal == codigoPostal).FirstOrDefault(); ;
        //    return Request.CreateResponse(HttpStatusCode.OK, new { localidad });
        //}
    }
}
