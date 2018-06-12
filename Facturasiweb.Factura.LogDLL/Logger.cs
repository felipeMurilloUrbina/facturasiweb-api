using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.LogDLL
{
    public class Logger
    {
        private string _ruta = "~/App_Data/errores";
        public Logger(string ruta)
        {
            this._ruta = ruta;
            if (!Directory.Exists(_ruta))
                Directory.CreateDirectory(_ruta);

        }

        public void EscribirError(string error, string usuario = "")
        {
            try
            {
                StreamWriter Adestino;
                string rutaCompleta = Path.Combine(_ruta, string.Format("Log.{3}.{0}.{1}.{2}.txt", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, usuario));
                Adestino = File.AppendText(rutaCompleta);
                Adestino.WriteLine(string.Format("Error:{0} hora: {1} ", error, DateTime.Now.ToLongTimeString()));
                Adestino.Close();
            }
            catch
            {
            }
        }
    }
}
