using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Facturasiweb.Factura.DAO;
using Facturasiweb.Factura.Model;
using System.Net.Http.Headers;
using AutoMapper;
using Facturasiweb.Factura.Modelo.DTO;
using Facturasiweb.Factura.Modelo;
using Facturasiweb.Factura.LogDLL;

namespace Facturasiweb.Factura.Api.Servicios
{
    public class Util
    {
        static readonly string PasswordHash = "P@@Sw0rd";
        static readonly string SaltKey = "S@LT&DEAK";
        private const string _alg = "HmacSHA256";
        static readonly string VIKey = "@1B2c3D4e5F6g7H8";
        public static int _expirationMinutes = 60 * 24;
        public static UsuarioDto USUARIO = null;
        private const string Secret = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";
        public static string GetHashedPassword(string password)
        {
            string key = string.Join(":", new string[] { password, SaltKey });
            using (HMAC hmac = HMACSHA256.Create(VIKey))
            {
                hmac.Key = Encoding.UTF8.GetBytes(SaltKey);
                hmac.ComputeHash(Encoding.UTF8.GetBytes(key));
                return Convert.ToBase64String(hmac.Hash);
            }
        }

        public static string GenerateToken(UsuarioDto usuario, long expireMinutes = 20)
        {
            var symmetricKey = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();
            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("user", Newtonsoft.Json.JsonConvert.SerializeObject(usuario,Formatting.Indented,  new JsonSerializerSettings() {
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    }    ))
                    }),

                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);
            return token;
        }

        public static bool IsTokenValid(string modulo, string accion, HttpRequestHeaders headers)
        {
            bool result = false;
            try
            {
                string token = headers.GetValues("token").FirstOrDefault();

                if (token == null)
                    token = headers.GetValues("Token").FirstOrDefault();
                if (token == null)
                    return result;
                USUARIO = Util.DecryptToken(headers);
                if (USUARIO != null)
                    result = true;
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        public static UsuarioDto DecryptToken(HttpRequestHeaders headers)
        {
            var token = headers.GetValues("token").FirstOrDefault();
            if (token == null)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            var data = jwtToken.Claims.Where(c => c.Type == "user").FirstOrDefault().Value;
            UsuarioDto usuario = JsonConvert.DeserializeObject<UsuarioDto>(data);
            return usuario;
        }

        public static string Encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            byte[] cipherTextBytes;
            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }

        public static string Decrypt(string encryptedText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }

        public static UsuarioDto mappingUsuario(Usuario usuario)
        {
            return Mapper.Map<Usuario, UsuarioDto>(usuario);
        }
    }
}