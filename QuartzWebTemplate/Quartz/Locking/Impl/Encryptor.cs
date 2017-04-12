using System;
using System.Security.Cryptography;
using System.Text;
using QuartzWebTemplate.Configuration;
using QuartzWebTemplate.Quartz.Locking.Contracts;

namespace QuartzWebTemplate.Quartz.Locking.Impl
{
    public class Encryptor : IEncryptor
    {
        private readonly IConfigurationProvider _configurationProvider;
        private static byte[] _key;
        private static byte[] _vector;

        public Encryptor(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
            _key = GetMd5Hash(GetKey());
            _vector = GetMd5Hash(GetVector());
        }

        private string GetKey()
        {
            return _configurationProvider.Get(ConfigurationKeys.EncryptKey);
        }

        private string GetVector()
        {
            return _configurationProvider.Get(ConfigurationKeys.EncryptVector);
        }

        public string Encrypt(string val)
        {
            if (string.IsNullOrWhiteSpace(val))
            {
                return null;
            }

            var rjm = new RijndaelManaged
            {
                KeySize = 128,
                BlockSize = 128,
                Key = _key,
                IV = _vector
            };

            var input = Encoding.UTF8.GetBytes(val);
            var output = rjm.CreateEncryptor().TransformFinalBlock(input, 0, input.Length);

            var data = Convert.ToBase64String(output);

            return data;
        }

        public string Decrypt(string val)
        {
            if (string.IsNullOrWhiteSpace(val))
            {
                return null;
            }

            try
            {
                var rjm = new RijndaelManaged
                {
                    KeySize = 128,
                    BlockSize = 128,
                    Key = _key,
                    IV = _vector
                };

                var input = Convert.FromBase64String(val);
                var output = rjm.CreateDecryptor()
                    .TransformFinalBlock(input, 0, input.Length);
                var data = Encoding.UTF8.GetString(output);

                return data;
            }
            catch
            {
                return null;
            }
        }

        private static byte[] GetMd5Hash(string data)
        {
            return MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(data));
        }
    }
}