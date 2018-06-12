using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Facturasiweb.Factura.Api.Servicios;
using Facturasiweb.Factura.BLL;
using Facturasiweb.Factura.DAO;

namespace Facturasiweb.Factura.Api.Controllers
{
    [RoutePrefix("api/turnos"), CustomAuthorize]
    public class TurnosController : BaseController
    {
        public ServicioTurno _servicio = null;
        public TurnosController(Context contexto) : base(contexto)
        {
            this._servicio = new ServicioTurno(this._logger, this._contexto);
        }
        public IHttpActionResult Get()
        {
            var _sucursal = Request.Headers.GetValues("sucursal").FirstOrDefault();
            return Ok(_servicio.Get(int.Parse(_sucursal)));
        }
    }
}
