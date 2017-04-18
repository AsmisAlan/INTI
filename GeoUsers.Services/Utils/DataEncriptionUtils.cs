using System.Text;
using System.Security.Cryptography;
using System;

namespace GeoUsers.Services.Utils
{
    public static class DataEncriptionUtils
    {
        private const string SecretKey = "SGUAPP001";

        public static string Encrypt(string plainText)
        {
            plainText += SecretKey;

            var data = Encoding.ASCII.GetBytes(plainText);

            data = new SHA256Managed().ComputeHash(data);

            var hash = Convert.ToBase64String(data);

            return hash;
        }
    }
}