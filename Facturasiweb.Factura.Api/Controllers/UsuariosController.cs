using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using Facturasiweb.Factura.Model;
using Facturasiweb.Factura.Api.Servicios;
using Facturasiweb.Factura.DAO;
using Facturasiweb.Factura.BLL;
using Facturasiweb.Factura.BLL.Modelos;
using Facturasiweb.Factura.Modelo;

namespace Facturasiweb.Factura.Api.Controllers
{
    [RoutePrefix("api/usuarios")]
    public class UsuariosController : BaseController
    {
        private ServicioUsuario _servicio = null;
        public UsuariosController(Context contexto) : base(contexto)
        {
            this._servicio = new ServicioUsuario(this._logger, contexto);
        }
        [Route(""),HttpGet]
        public IHttpActionResult Get()
        { 
            return Ok(_servicio.Get(Util.USUARIO));
        }
        [AllowAnonymous, Route("login"), HttpPost]
        public IHttpActionResult Login([FromBody]Usuario Usuario)
        {
            var _usuarioLogin = _servicio.Login(ref _error, Usuario);
            if (_usuarioLogin == null)
                return BadRequest(_error);
            else
                return Ok(_usuarioLogin);
        }
        [Route(""), CustomAuthorize, HttpPost]
        public IHttpActionResult Post([FromBody]Usuario usuario)
        {
            if (_servicio.Post(ref _error, usuario, Util.USUARIO))
                return Ok("Usuario Guardado Correctamente");
            else
                return BadRequest(_error);
        }
        [CustomAuthorize, Route(""), HttpPut]
        public IHttpActionResult Put([FromBody]Usuario usuario)
        {
            if (_servicio.Put(ref _error, usuario))
                return Ok("Usuario Guardado Correctamente");
            else
                return BadRequest(_error);
        }
        [CustomAuthorize, HttpDelete]
        public IHttpActionResult Delete([FromBody]Usuario usuario)
        {
            if (_servicio.Put(ref _error, usuario))
                return Ok("Usuario Eliminado Correctamente");
            else
                return BadRequest(_error);
        }
        
        //[AllowAnonymous, Route("registrar"), HttpPost]
        //public IHttpActionResult Register([FromBody]Usuario Usuario)
        //{
        //    var isValid = Usuario == null ? false : true;
        //    if ((!isValid))
        //    {
        //        return BadRequest("Datos incorrectos");
        //    }
        //    string errors = PasswordVerify.ValidationRules(Usuario.Contra);
        //    if (errors.Length == 0)
        //    {
        //        var u = _context.Usuarios.Where(us => us.NombreUsuario == Usuario.NombreUsuario).FirstOrDefault();
        //        if (u != null)
        //            errors += "Ya existe un registro con ese usuario.,";
        //    }

        //    var listErrors = errors.Split(',');
        //    if (errors.Length > 0)
        //        return BadRequest(errors);

        //    Usuario.Contra = Util.Encrypt(Usuario.Contra);
        //    _context.Entry(Usuario).State = System.Data.Entity.EntityState.Added;
        //    try
        //    {
        //        _context.SaveChanges();
        //    }
        //    catch
        //    {

        //    }
        //    return Ok(Usuario);
        //}



        //[CustomAuthorize, Route("{id}/password"), HttpPost]
        //public HttpResponseMessage PostConfiguracion([FromUri]int id, [FromBody]string password)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, new { Mensaje = "Error datos no validos" });
        //    }
        //    var token = Request.Headers.GetValues("token").First();
        //    var uLogeado = Util.DecryptToken(token);
        //    if (!uLogeado.Tipo.Contains("Admin"))
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, new { Mensaje = "El Usuario no tiene permisos." });
        //    }
        //    var usuarioGuardar = _context.Usuarios.Where(u => u.Id == id).FirstOrDefault();
        //    usuarioGuardar.Contra = Util.Encrypt(password);
        //    _context.Entry(usuarioGuardar).State = EntityState.Modified;
        //    _context.SaveChanges();
        //    return Request.CreateResponse(HttpStatusCode.OK, new { usuarioGuardar });
        //}
    }
}
