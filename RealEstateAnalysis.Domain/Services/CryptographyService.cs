using RealEstateAnalysis.Domain.Abstract;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RealEstateAnalysis.Domain.Services
{
    public class CryptographyService : ICryptographyService
    {
        private readonly byte[] rgbKey = Convert.FromBase64String("JpLGrLUtYIP1fIq4nXdV2aC9Kv/bZVz6xI+o9G117tU=");
        private readonly byte[] rgbInitVector = Convert.FromBase64String("jxyBCpgorMollcMUSntzIA==");

        public string EncryptText(string clearText)
        {
            using (var provider = new RijndaelManaged())
            {
                var clearBytes = Encoding.UTF8.GetBytes(clearText);
                var foggyBytes = Trasform(clearBytes, provider.CreateEncryptor(rgbKey, rgbInitVector));

                return Convert.ToBase64String(foggyBytes);
            }
        }

        public string DecryptText(string ecryptedText)
        {
            var decryptedText = string.Empty;
            var foggyBytes = Convert.FromBase64String(ecryptedText);

            using (var provider = new RijndaelManaged())
                decryptedText = Encoding.UTF8.GetString(Trasform(foggyBytes, provider.CreateDecryptor(rgbKey, rgbInitVector)));

            return decryptedText;
        }

        private byte[] Trasform(byte[] textBytes, ICryptoTransform transform)
        {
            using (var memoryStream = new MemoryStream())
            using (var cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write))
            {
                cryptoStream.Write(textBytes, 0, textBytes.Length);
                cryptoStream.FlushFinalBlock();

                return memoryStream.ToArray();
            }
        }
    }
}