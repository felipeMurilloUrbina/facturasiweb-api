using Facturasiweb.Factura.DAO;
using Facturasiweb.Factura.LogDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.BLL
{
    public class ServicioBase
    {
        public Logger _logger = null;
        public Context _contexto = null;

        public ServicioBase(Logger logger, Context contexto)
        {
            this._logger = logger;
            this._contexto = contexto;
        }
    }
}
