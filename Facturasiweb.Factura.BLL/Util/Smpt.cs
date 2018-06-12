using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.BLL.Util
{
    public class Smtp : SmtpClient
    {
        public Smtp()
        {
            this.Port = 8889;
            this.Credentials = new System.Net.NetworkCredential("noresponder@facturasiweb.com", "a123.456");
            this.Host = "mail.facturasiweb.com";
        }

        public string EnviarCorreo(MailMessage Mensaje)
        {
            string respuesta = "Se envio correctamente la factura";
            try
            {
                this.Send(Mensaje);
            }
            catch (Exception e)
            {
                respuesta = "Error al enviar correo, revise configuraciones";
            }
            return respuesta;
        }

    }
}
