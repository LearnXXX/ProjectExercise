using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto.Digests;

namespace BouncyCastleTest.Hash
{
    internal class MD5Test
    {
        public static void Start()
        {
            var plaintext = "这是一次MD5测试";

            var digest = new MD5Digest();
            var hash = new byte[digest.GetDigestSize()];
            var data = Encoding.UTF8.GetBytes(plaintext);
            digest.BlockUpdate(data, 0, data.Length);
            digest.DoFinal(hash, 0);
            var value = Convert.ToBase64String(hash);



            using (MD5 md5Hash = MD5.Create())
            {
                // 将输入字符串转换为字节数组并计算其哈希数据
                byte[] md5Value = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(plaintext));

                var md5Text = Convert.ToBase64String(md5Value);
                if (string.Equals(value, md5Text) == false)
                {
                    throw new Exception("Md5 Falied");
                }

            }
        }
    }
}
