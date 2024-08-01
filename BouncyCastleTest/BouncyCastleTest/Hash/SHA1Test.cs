using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto.Digests;

namespace BouncyCastleTest.Hash
{
    internal class SHA1Test
    {
        public static void Start()
        {
            var plaintext = "这是一次Sha1测试";

            var digest = new Sha1Digest();
            var hash = new byte[digest.GetDigestSize()];
            var data = Encoding.UTF8.GetBytes(plaintext);
            digest.BlockUpdate(data, 0, data.Length);
            digest.DoFinal(hash, 0);
            var value = Convert.ToBase64String(hash);

            using (var sha1Hash = SHA1.Create())
            {
                // 将输入字符串转换为字节数组并计算其哈希数据
                byte[] sha1Value = sha1Hash.ComputeHash(Encoding.UTF8.GetBytes(plaintext));

                var sha1Text = Convert.ToBase64String(sha1Value);
                if (string.Equals(value, sha1Text) == false)
                {
                    throw new Exception("SHA1 Falied");
                }

            }
        }
    }
}
