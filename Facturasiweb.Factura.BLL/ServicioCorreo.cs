using Facturasiweb.Factura.BLL.Util;
using Facturasiweb.Factura.LogDLL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Facturasiweb.Factura.BLL
{
    public static class ServicioCorreo
    {
        public static void Enviar(string asunto, string mensaje, String correos, string rutaPDF, string rutaXML, Logger logger)
        {
            Task.Run(() =>
            {
                List<Object> Correos = null;
                try
                {
                    Correos = JsonConvert.DeserializeObject<List<object>>(correos);
                    if (Correos != null)
                    {
                        if (Correos.Count() > 0)
                            new Smtp().EnviarCorreo(ServicioCorreo.GetMensaje(asunto, mensaje, rutaPDF, rutaXML, Correos, logger));
                    }
                }
                catch (Exception e)
                {
                    logger.EscribirError(e.ToString(), "Enviar Correo");
                }

            });
        }

        public static MailMessage GetMensaje(string asunto, string mensaje, string rutaPDF, string rutaXML, List<object> destinatarios, Logger logger)
        {
            MailMessage mail = new MailMessage();
            try
            {
                mail.From = new MailAddress("noresponder@facturasiweb.com");
                mail.Attachments.Add(new Attachment(rutaXML, System.Net.Mime.MediaTypeNames.Application.Octet));
                mail.Attachments.Add(new Attachment(rutaPDF, System.Net.Mime.MediaTypeNames.Application.Pdf));
                mail.Subject = asunto;
                mail.Body = mensaje;
                mail.IsBodyHtml = true;
                foreach (var dest in destinatarios)
                {
                    if (!string.IsNullOrEmpty(dest.ToString()))
                        mail.To.Add(dest.ToString().Trim());
                }
            }
            catch (Exception e)
            {
                logger.EscribirError(e.ToString(), "Crear Mensaje");
            }
            return mail;
        }

    }
}
