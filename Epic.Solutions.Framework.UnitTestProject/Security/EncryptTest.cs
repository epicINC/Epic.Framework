using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;
using System.Text;
using Epic.Extensions;

namespace Epic.Solutions.Framework.UnitTestProject.Security
{
    [TestClass]
    public class EncryptTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var text = "11111";

            var enocede = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(Encoding.UTF8.GetBytes(text));

            Assert.AreEqual(enocede.ToHexString(), BitConverter.ToString(enocede).Replace("-", String.Empty));
        }
    }
}
