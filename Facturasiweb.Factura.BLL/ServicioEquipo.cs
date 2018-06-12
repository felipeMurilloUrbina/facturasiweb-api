using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facturasiweb.Factura.DAO;
using Facturasiweb.Factura.LogDLL;
using Facturasiweb.Factura.Modelo;
using Facturasiweb.Factura.Modelo.DTO;

namespace Facturasiweb.Factura.BLL
{
    public class ServicioEquipo : ServicioBase
    {
        public ServicioEquipo(Logger logger, Context contexto) : base(logger, contexto)
        {
        }

        public ICollection<Equipo> Get(int usuarioId)
        {
            return this._contexto.Equipos.Where(s => s.UsuarioId == usuarioId).ToList();
        }
        public Equipo GetId(int EquipoId)
        {
            return this._contexto.Equipos.Find(EquipoId);
        }

        public Boolean Post(ref string error, Equipo equipo, UsuarioDto usuario)
        {
            equipo.UsuarioCreadorId = usuario.Id;
            equipo.UsuarioModificadorId = usuario.Id;
            equipo.UsuarioId = usuario.UsuarioSistemaId;
            this._contexto.Equipos.Add(equipo);
            try
            {
                this._contexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                this._logger.EscribirError(equipo.UsuarioId.ToString(), e.ToString());
                error = "Ocurrio un problema al querer guardar";
                return false;
            }
        }

        public Boolean Put(ref string error, Equipo Equipo, UsuarioDto usuario)
        {
            this._contexto.Entry(Equipo).State = EntityState.Modified;
            try
            {
                this._contexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                this._logger.EscribirError(Equipo.UsuarioId.ToString(), e.ToString());
                error = "Ocurrio un problema al querer guardar";
                return false;
            }
        }
    }
}
