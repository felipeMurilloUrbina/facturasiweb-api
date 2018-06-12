using Facturasiweb.Factura.BLL.Servicios;
using Facturasiweb.Factura.DAO;
using Facturasiweb.Factura.LogDLL;
using Facturasiweb.Factura.Modelo;
using Facturasiweb.Factura.Modelo.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace Facturasiweb.Factura.BLL
{
    public class ServicioSucursal : ServicioBase
    {
        public ServicioSucursal(Context contexto, Logger logger) : base(logger, contexto)
        {
        }
        public ICollection<Sucursal> Get(int usuarioId)
        {
            return this._contexto.Sucursales.Where(s => s.UsuarioId == usuarioId).ToList();
        }
        public Sucursal GetId(int sucursalId)
        {
            return this._contexto.Sucursales.Include(s => s.Regimenes).Include("Regimenes.Regimen") .Where(s => s.Id == sucursalId).FirstOrDefault();
        }
        public Boolean Post(ref string error, Sucursal sucursal, UsuarioDto usuario)
        {
            sucursal.Serie = sucursal.Serie ?? "FA";
            sucursal.UsuarioCreadorId = usuario.Id;
            sucursal.UsuarioModificadorId = usuario.Id;
            sucursal.UsuarioId = usuario.UsuarioSistemaId;
            this._contexto.Sucursales.Add(sucursal);
            try
            {
                this._contexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                this._logger.EscribirError(sucursal.UsuarioId.ToString(), e.ToString());
                error = "Ocurrio un problema al querer guardar";
                return false;
            }
        }
        public Boolean Put(ref string error, Sucursal sucursal)
        {
            sucursal.Activo = true;
            this._contexto.Database.ExecuteSqlCommand("delete from SucursalRegimenes where sucursalId={0}", sucursal.Id);
            foreach (var regimen in sucursal.Regimenes)
            {
                this._contexto.Entry(regimen).State = EntityState.Added;
            }
            this._contexto.Entry(sucursal).State = EntityState.Modified;
            try
            {
                this._contexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                this._logger.EscribirError(sucursal.UsuarioId.ToString(), e.ToString());
                error = "Ocurrio un problema al querer guardar";
                return false;
            }
        }

        public Boolean UploadFile(ref string error, HttpContext contexto, Sucursal sucursal)
        {
            ServicioArchivo _servicioArchivo = new ServicioArchivo(this._logger);
            Ruta _ruta = new Ruta(contexto);
            var _archivo = contexto.Request.Files[0];
            _servicioArchivo.CrearCarpeta(_ruta.RutaCarpetaArchivosSucursal(sucursal.Id.ToString(), sucursal.Rfc, "imagenes"));
            _servicioArchivo.CrearCarpeta(_ruta.RutaCarpetaArchivosSucursal(sucursal.Id.ToString(), sucursal.Rfc, "certificados"));
            _archivo.SaveAs(_servicioArchivo.GetNombre(ref sucursal, _ruta, Path.GetExtension(_archivo.FileName)));
            return Put(ref error, sucursal);
        }
    }
}
