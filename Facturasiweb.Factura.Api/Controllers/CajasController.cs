using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Facturasiweb.Factura.DAO;

namespace Facturasiweb.Factura.Api.Controllers
{
    public class CajasController : BaseController
    {
        public CajasController(Context contexto) : base(contexto)
        {
        }
    }
}
