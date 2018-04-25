using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Epic.Framework.Testing;
using System.IO;
using System.Security.Cryptography;

namespace Epic.Framework.ConsoleApplication
{
    public class AzDGXXTEATest
    {
        public static void TS(byte[] value, int loop)
        {
            TestingUtility.SpeedTest(AzDGTest, value, loop);
            TestingUtility.SpeedTest(XXTEATest, value, loop);
            TestingUtility.SpeedTest(AESTest, value, loop);
        }

        static readonly byte[] key = Encoding.UTF8.GetBytes("uTTKMuC7cKaVNWtJtqvZJo7QhlnpPUZr");
        static readonly byte[] Salt = Encoding.UTF8.GetBytes("xU9Z2uJtxU9Z2uJt");

        public static void AzDGTest(byte[] value)
        {
            Epic.Security.Cryptography.AzDG.Encrypt(key, value);
        }

        public static void XXTEATest(byte[] value)
        {
            Epic.Security.Cryptography.XXTEA.Encrypt(key, value);
        }

        public static void AESTest(byte[] value)
        {
            var aes = new AesManaged();
            aes.Key = key;
            aes.IV = Salt;

            var encryptTransform = aes.CreateEncryptor();

            var encryptStream = new MemoryStream();
            var encryptor = new CryptoStream(encryptStream, encryptTransform, CryptoStreamMode.Write);


            encryptor.Write(value, 0, value.Length);
            encryptor.Close();
        }
    }
}
