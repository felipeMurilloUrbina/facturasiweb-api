//using Api.Custom;
//using Api.Servicios;
//using Fact.Api.Custom;
//using FACT.DAO.Contexto;
//using FACT.DAO.DTO;
//using FACT.DAO.Modelos.PuntoVenta;
//using Facturasiweb.Factura.Api.Servicios;
//using Facturasiweb.Factura.Model.PuntoVenta;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;
//using System.Web.Http.Description;

//namespace Facturasiweb.Factura.Api.Controllers
//{
//    [RoutePrefix("api/caja")]
//    public class CajaController : ApiController
//    {
//        private readonly Context db = null;
//        public CajaController(Context context)
//        {
//            this.db = context;
//        }
//        [CustomAuthorize, Route("{sucursalId}"), HttpGet]
//        public IHttpActionResult GetCajas(int sucursalId)
//        {
//            var token = Request.Headers.GetValues("token").First();
//            var uLogeado = Util.DecryptToken(token);
//            return Ok(getCajas(sucursalId));
//        }

//        [CustomAuthorize, Route("{sucursalId}/{turnoId}/disponibles"), HttpGet]
//        public IHttpActionResult GetCajasDisponibles(int sucursalId, int turnoId)
//        {
//            var token = Request.Headers.GetValues("token").First();
//            var uLogeado = Util.DecryptToken(token);
//            var cajas = getCajas(sucursalId);
//            List<CajaDto> cajasDisponibles = new List<CajaDto>();
//            var cajasOcupadas = db.TurnoCajas.Where(tc => tc.SucursalId == sucursalId && tc.TurnoId == turnoId).ToList();
//            foreach (var caja in cajas)
//            {
//                if (cajasOcupadas.Where(c => c.CajaId == caja.Id).FirstOrDefault() == null)
//                    cajasDisponibles.Add(caja);
//            }
//            return Ok(cajasDisponibles);
//        }

//        // GET: api/Sucursal/5
//        [CustomAuthorize, Route("{sucursalId}/{id}"), HttpGet]
//        [ResponseType(typeof(Caja))]
//        public IHttpActionResult GetCaja(int sucursalId, int id)
//        {
//            Caja Caja = db.Cajas.Find(id);
//            if (Caja == null)
//            {
//                return NotFound();
//            }
//            return Ok(Caja);
//        }

//        //[CustomAuthorize, Route("{sucursalId}/nueva"), HttpGet]
//        //[ResponseType(typeof(Caja))]
//        //public IHttpActionResult GetNuevaCaja(int sucursalId)
//        //{
//        //    var Cajas = db.Cajas.Where(f => f.SucursalId == sucursalId).ToList();
//        //    var Folio = Cajas.Count > 0 ? Cajas.Max(f => f.Folio) + 1 : 1;
//        //    return Ok(Folio);
//        //}

//        [CustomAuthorize, Route(""), HttpPut]
//        [ResponseType(typeof(void))]
//        public IHttpActionResult PutCaja([FromBody]Caja caja)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//            db.Entry(caja).State = EntityState.Modified;
//            db.Entry(caja).Property(p => p.SucursalId).IsModified = false;
//            db.Entry(caja).Property(p => p.Activo).IsModified = false;
//            try
//            {
//                db.SaveChanges();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                return NotFound();
//            }

//            return StatusCode(HttpStatusCode.NoContent);
//        }

//        // PUT: api/Cliente/5
//        [Route("{id}"), HttpPut]
//        [ResponseType(typeof(void))]
//        public IHttpActionResult PutCaja(int id, Caja caja)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            if (id != caja.Id)
//            {
//                return BadRequest();
//            }

//            db.Entry(caja).State = EntityState.Modified;
//            db.Entry(caja).Property(p => p.SucursalId).IsModified = false;

//            try
//            {
//                db.SaveChanges();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                return NotFound();
//            }
//            return StatusCode(HttpStatusCode.NoContent);
//        }

//        [CustomAuthorize, Route(""), HttpPost]
//        public IHttpActionResult PostCaja(Caja caja)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest("Modelo Invalido");
//            }
//            var token = Request.Headers.GetValues("token").First();
//            var uLogeado = Util.DecryptToken(token);
//            caja.UsuarioId = uLogeado.Id;
//            caja.Fecha = DateTime.Now;
//            caja.Activo = true;
//            db.Cajas.Add(caja);
//            db.SaveChanges();

//            return Ok("Caja Guardada Correctamente");
//        }

//        private List<CajaDto> getCajas(int SucursalId)
//        {
//            return ServicioMap.MappingCajas(db.Cajas.Where(s => s.SucursalId == SucursalId).ToList());
//        }

//        private bool ClienteExists(int id)
//        {
//            return db.Clientes.Count(e => e.Id == id) > 0;
//        }
//    }
//}
