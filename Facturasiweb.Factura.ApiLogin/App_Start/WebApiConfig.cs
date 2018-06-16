using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;

namespace Facturasiweb.Factura.ApiLogin.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
        }
    }
}