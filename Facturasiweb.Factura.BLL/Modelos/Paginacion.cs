using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Facturasiweb.Factura.Api.Models
{
    public class Paginacion<T> where T: class
    {
        public int PaginaActual { get; set; }

        public int ElementosPagina { get; set; }

        public int ElementosTotales { get; set; }

        public int PaginasTotales { get; set; }

        public ICollection<T> Elementos { get; set; }
    }
}