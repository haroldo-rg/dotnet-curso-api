using Curso.Api.Business.Tools;
using Microsoft.Extensions.Configuration;
using System;

namespace Curso.Api.Infrastructure.Tools
{
    public class Criptografia : ICriptografia
    {
        private readonly string EncryptKey;
        private readonly string EncryptIV;

        public Criptografia(IConfiguration configuration)
        {
            EncryptKey = configuration.GetSection("RijndaelManagedCryptography:EncryptKey").Value;
            EncryptIV = configuration.GetSection("RijndaelManagedCryptography:EncryptIV").Value;
        }

        public string Encrypt(string texto)
        {
            return Convert.ToBase64String(Tools.RijndaelManagedCryptography.EncryptString(texto, EncryptKey, EncryptIV)); ;
        }

        public string Decrypt(string texto)
        {
            return Tools.RijndaelManagedCryptography.DecryptString(Convert.FromBase64String(texto), EncryptKey, EncryptIV);
        }

    }
}
