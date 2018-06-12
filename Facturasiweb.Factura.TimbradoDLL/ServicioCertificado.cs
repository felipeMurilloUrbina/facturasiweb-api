
using Facturasiweb.Factura.LogDLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.OpenSsl;
using Facturasiweb.Factura.TimbradoDLL.Properties;

namespace Facturasiweb.Factura.TimbradoDLL
{
    public class ServicioCertificado
    {
        Logger _logger = null;
        public ServicioCertificado(Logger logger)
        {
            this._logger = logger;
        }
        /// <summary>
        /// Retorna el certificado para el usuario especificado
        /// </summary>
        /// <param name="rutaGuardado"></param>
        /// <param name="rutaCertificado"></param>
        /// <param name="nombreCertificado"></param>
        /// <returns></returns>
        public string GetCertificado(string rutaGuardado, string rutaCertificado, string nombreCertificado)
        {
            string certificado = string.Empty;
            try
            {
                var rutaCerPem = String.Format(@"{0}\{1}{2}", rutaGuardado, nombreCertificado, ".pem");
                CreaArchivoExtPEM(rutaCertificado, rutaCerPem);
                string certificadoOriginal = File.ReadAllText(rutaCerPem);
                certificadoOriginal = certificadoOriginal.Substring(28);
                certificadoOriginal = certificadoOriginal.Replace('\r', ' ');
                certificadoOriginal = certificadoOriginal.Replace('\n', ' ');
                certificadoOriginal = certificadoOriginal.Replace("END CERTIFICATE", "");
                for (int i = 0; i < certificadoOriginal.Length; i++)
                {
                    if ((certificadoOriginal[i] != '-') && (!char.IsWhiteSpace(certificadoOriginal[i])))
                        certificado += certificadoOriginal[i];
                }
            }
            catch(Exception e) 
            {
                _logger.EscribirError(e.ToString());
            }

            return certificado;
        }

        /// <summary>
        /// Retorna el numero de certificado para el usuario especificado.
        /// </summary>
        /// <param name="rutaCertificado"></param>
        /// <returns></returns>
        public string GetNumeroCertificado(ref string error, string rutaCertificado)
        {
            string resultado = "";
            try
            {
                X509Certificate2 x509 = new X509Certificate2(rutaCertificado);
                for (int i = 0; i < x509.SerialNumber.Length; i++)
                {
                    int x = i + 1;
                    if (((i + 1) % 2) == 0)
                    {
                        resultado += x509.SerialNumber[i];
                    }
                }
            }
            catch (Exception e)
            {
                error = "No se encontro la ruta del certificado";
                _logger.EscribirError(e.ToString());
            }
            return resultado;
        }
        public string GetCadenaOriginal(ref string error, string rutaXml, string rutaXSLT)
        {
            string respuesta = "";
            try
            {
                XPathDocument xml = new XPathDocument(rutaXml);
                XslCompiledTransform transformador = new XslCompiledTransform();
                transformador.Load(rutaXSLT);
                StringWriter resultado = new StringWriter();
                XmlTextWriter cad = new XmlTextWriter(resultado);
                transformador.Transform(rutaXml, cad);
                if (String.IsNullOrEmpty(resultado.ToString()))
                    error = "Problemas al generar cadena original problemas con los datos.";
                 respuesta= resultado.ToString();
            }
            catch (Exception e)
            {
                error = "Ocurrio un problema al obtener la cadena Original.";
                this._logger.EscribirError(e.ToString());
            }
            return respuesta;
        }
        /// <summary>
        /// Retorna el sello digital para el certificado y firmando con la llave.
        /// </summary>
        /// <param name="rutaLlave"></param>
        /// <param name="clavePrivada"></param>
        /// <param name="cadenaOriginal"></param>
        /// <returns></returns>
        public string GetSelloDigital(ref string error, string rutaLlave, string clavePrivada, string cadenaOriginal)
        {
            try
            {
                byte[] ArrayKey = File.ReadAllBytes(rutaLlave); // Convertimos el archivo anterior a byte
                //1) Desencriptar la llave privada, el primer parámetro es la contraseña de llave privada y el segundo es la llave privada en formato binario.
                Org.BouncyCastle.Crypto.AsymmetricKeyParameter asp = Org.BouncyCastle.Security.PrivateKeyFactory.DecryptKey(clavePrivada.ToCharArray(), ArrayKey);
                //2) Convertir a parámetros de RSA
                Org.BouncyCastle.Crypto.Parameters.RsaKeyParameters key = (Org.BouncyCastle.Crypto.Parameters.RsaKeyParameters)asp;
                //3) Crear el firmador con SHA1
                Org.BouncyCastle.Crypto.ISigner sig = Org.BouncyCastle.Security.SignerUtilities.GetSigner("SHA-256withRSA");
                //La siguiente linea es para generar el sello para la nueva versión de CFDI 3.3
                // Org.BouncyCastle.Crypto.ISigner sig = Org.BouncyCastle.Security.SignerUtilities.GetSigner("SHA-256withRSA");
                //4) Inicializar el firmador con la llave privada
                sig.Init(true, key);
                // 5) Pasar la cadena original a formato binario
                byte[] bytes = Encoding.UTF8.GetBytes(cadenaOriginal);
                // 6) Encriptar
                sig.BlockUpdate(bytes, 0, bytes.Length);
                byte[] bytesFirmados = sig.GenerateSignature();
                // 7) Finalmente obtenemos el sello
                String sello = Convert.ToBase64String(bytesFirmados);
                return sello;
            }
            catch(Exception e )
            {
                _logger.EscribirError(e.ToString());
                error = "Problemas al generar xml para el timbrado, favor revisar clave o certificados.";
                return "";
            }

        }
        /// <summary>
        /// Crea un archivo .PEM partiendo del certificado.
        /// </summary>
        /// <param name="rutaCertificado"></param>
        /// <param name="RutaCerticadoPEM"></param>
        public void CreaArchivoExtPEM(String rutaCertificado, String RutaCerticadoPEM)
        {
            if (!File.Exists(RutaCerticadoPEM))
            {
                Stream sr = File.OpenRead(rutaCertificado);
                X509CertificateParser cp = new X509CertificateParser();
                Org.BouncyCastle.X509.X509Certificate cert = cp.ReadCertificate(sr);
                Object pkey = cert.GetPublicKey();
                TextWriter tw = new System.IO.StreamWriter(RutaCerticadoPEM);
                Org.BouncyCastle.OpenSsl.PemWriter pw = new Org.BouncyCastle.OpenSsl.PemWriter(tw);
                pw.WriteObject(cert);
                tw.Close();
            }
        }

        public void CreaArchivoKeyPEM(String archivoOrigen, String archivoDestino, String passCSDoFiel)
        {
            if (File.Exists(archivoDestino))
                File.Delete(archivoDestino);
            try
            {
                Byte[] DatosArchivoOrigen = File.ReadAllBytes(archivoOrigen);
                AsymmetricKeyParameter asp = Org.BouncyCastle.Security.PrivateKeyFactory.DecryptKey(passCSDoFiel.ToCharArray(), DatosArchivoOrigen);
                //Org.BouncyCastle.Security.PrivateKeyFactory.DecryptKey(passCSDoFiel.ToCharArray(), dataKey);
                MemoryStream ms = new MemoryStream();
                TextWriter writer = new StreamWriter(ms);
                TextWriter stWrite = new System.IO.StreamWriter(archivoDestino);
                PemWriter pmw = new PemWriter(stWrite);
                pmw.WriteObject(asp, "DESEDE", Resources.ContraFinkok.ToCharArray(), new SecureRandom());
                stWrite.Close();
            }
            catch (Exception e )
            {
                this._logger.EscribirError(e.ToString());
            }
            
        }
    }
}
