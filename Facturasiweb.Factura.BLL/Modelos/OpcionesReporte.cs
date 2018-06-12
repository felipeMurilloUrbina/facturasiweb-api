using Facturasiweb.Factura.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.BLL.Modelos
{
    public class OpcionesReporte
    {
        public List<Cliente> Clientes { get; set; }
        public List<MetodoPago> MetodosPago { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
    }
}
