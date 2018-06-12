using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Facturasiweb.Factura.Api.Servicios
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {

        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (AuthorizeRequest(actionContext))
                return;
            else
                HandleUnauthorizedRequest(actionContext);
        }

        protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            base.HandleUnauthorizedRequest(actionContext);
        }

        private bool AuthorizeRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var modulo = actionContext.Request.GetRouteData().Route.RouteTemplate;
            var accion = actionContext.Request.Method.Method;
                return Util.IsTokenValid(modulo, accion, actionContext.Request.Headers);
        }

    }
}