using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace BouncyCastleTest.Encryption
{
    internal class AESTest
    {
        public static void Start()
        {
            var plaintext = "这是一次AES加密测试";
            string iv = Guid.NewGuid().ToString("N");
            if (iv.Length > 16)
            {
                iv = iv.Substring(0, 16);
            }


            string key = "aiM5Qsbg3agkvw8b";
            var cipher = CipherUtilities.GetCipher("AES/CBC/PKCS7Padding");
            cipher.Init(true, new ParametersWithIV(ParameterUtilities.CreateKeyParameter("AES", Encoding.UTF8.GetBytes(key)), Encoding.UTF8.GetBytes(iv)));
            var encryptMessage = Convert.ToBase64String(cipher.DoFinal(Encoding.UTF8.GetBytes(plaintext)));

            cipher.Init(false, new ParametersWithIV(ParameterUtilities.CreateKeyParameter("AES", Encoding.UTF8.GetBytes(key)), Encoding.UTF8.GetBytes(iv)));
            var decryptMessage = Encoding.UTF8.GetString(cipher.DoFinal(Convert.FromBase64String(encryptMessage)));
        }

    }
}
