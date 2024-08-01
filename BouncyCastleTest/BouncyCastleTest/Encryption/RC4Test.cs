using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;

namespace BouncyCastleTest.Encryption
{
    internal class RC4Test
    {
        public static void Start()
        {
            var plaintext = "这是一次RC4加密测试";
            string key = "riM5Qsbg3agkvw8b";
            var cipher = new RC4Engine();
            cipher.Init(true, new KeyParameter(Encoding.UTF8.GetBytes(key)));
            var data = Encoding.UTF8.GetBytes(plaintext);
            byte[] ciphertext = new byte[data.Length];
            cipher.ProcessBytes(Encoding.UTF8.GetBytes(plaintext), 0, data.Length, ciphertext, 0);
            var encryptText = Convert.ToBase64String(ciphertext);


            cipher.Init(false, new KeyParameter(Encoding.UTF8.GetBytes(key)));

            var encryptContent = Convert.FromBase64String(encryptText);
            byte[] decryptContent = new byte[encryptContent.Length];
            cipher.ProcessBytes(encryptContent, 0, encryptContent.Length, decryptContent, 0);
            var decryptText = Encoding.UTF8.GetString(decryptContent);
        }
    }
}
