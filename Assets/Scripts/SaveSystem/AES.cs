using System;
using System.Security.Cryptography;
using System.Text;

namespace SaveSystem
{
    public sealed class AES
    {
        private readonly AesCryptoServiceProvider _cryptoServiceProvider;
        private readonly ICryptoTransform _encryptor;
        private readonly ICryptoTransform _decryptor;


        public AES(string iv, string key)
        {
            _cryptoServiceProvider = new AesCryptoServiceProvider();

            _cryptoServiceProvider.BlockSize = 128;
            _cryptoServiceProvider.KeySize = 256;
            _cryptoServiceProvider.IV = GetIv(iv);
            _cryptoServiceProvider.Key = GetKey(key);
            _cryptoServiceProvider.Mode = CipherMode.CBC;
            _cryptoServiceProvider.Padding = PaddingMode.PKCS7;

            _encryptor = _cryptoServiceProvider.CreateEncryptor();
            _decryptor = _cryptoServiceProvider.CreateDecryptor();
        }

        public string Encrypt(string str)
        {
            var encrypted = _encryptor.TransformFinalBlock(Encoding.ASCII.GetBytes(str), 0, str.Length);
            return Convert.ToBase64String(encrypted);
        }
        public string Decrypt(string str)
        {
            var encrypted = Convert.FromBase64String(str);
            var decrypted = _decryptor.TransformFinalBlock(encrypted, 0, encrypted.Length);

            return Encoding.ASCII.GetString(decrypted);
        }
        
        private static byte[] GetIv(string ivSecret)
        {
            using var md5 = MD5.Create();
            return md5.ComputeHash(Encoding.UTF8.GetBytes(ivSecret));
        }
        private static byte[] GetKey(string key)
        {
            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
        }
    }
}