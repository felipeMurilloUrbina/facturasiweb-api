using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facturasiweb.Factura.DAO;
using Facturasiweb.Factura.LogDLL;
using Facturasiweb.Factura.Modelo.PuntoVenta;

namespace Facturasiweb.Factura.BLL
{
    public class ServicioTurno : ServicioBase
    {
        public ServicioTurno(Logger logger, Context contexto) : base(logger, contexto)
        {
        }

        public ICollection<Turno> Get(int sucursalId)
        {
            return this._contexto.Turnos.Where(t => t.SucursalId == sucursalId).ToList();
        }
        public Turno GetId(int id)
        {
            return this._contexto.Turnos.Where(t => t.Id == id).FirstOrDefault();
        }
        public Boolean Post(ref string error, Turno turno)
        {
            try
            {
                return true;
            }
            catch (Exception e)
            {
                error = "Error, al guardar el turno ";
                this._logger.EscribirError(e.ToString());
                return false;
            }
        }
    }
}
