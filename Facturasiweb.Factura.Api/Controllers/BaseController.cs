using Facturasiweb.Factura.Api.Servicios;
using Facturasiweb.Factura.DAO;
using Facturasiweb.Factura.LogDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Facturasiweb.Factura.Api.Controllers
{
    public class BaseController : ApiController
    {
        public Context _contexto = null;
        public Logger _logger = null;
        public string _error = "";
        public BaseController(Context contexto)
        {
            this._logger = new Logger(HttpContext.Current.Server.MapPath(Ruta.RutaError));
            this._contexto = contexto;
        }

    }
}
