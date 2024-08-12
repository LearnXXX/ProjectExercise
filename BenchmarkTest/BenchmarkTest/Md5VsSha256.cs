using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;

namespace BenchmarkTest
{
    //[SimpleJob(RuntimeMoniker.Net48)]
    //[SimpleJob(RuntimeMoniker.NetCoreApp50)]
    [Config(typeof(Config))]
    public class Md5VsSha256
    {
        private const int N = 10000;
        private readonly byte[] data;
        private readonly SHA256 sha256 = SHA256.Create();
        private readonly MD5 md5 = MD5.Create();

        public Md5VsSha256()
        {
            data = new byte[N];
            new Random(42).NextBytes(data);
        }

        [Params(10,20)]
        public int Property { get; set; }

        [Benchmark]
        public void Output()
        {
            Console.WriteLine(Property);
        }

        [Benchmark]
        public byte[] Sha256()=>sha256.ComputeHash(data);

        [Benchmark]
        public byte[] Md5() => md5.ComputeHash(data);

        private class Config : ManualConfig
        {
            public Config()
            {
                AddJob(new Job(Job.Dry)
                {
                    Environment = { Platform = BenchmarkDotNet.Environments.Platform.X64 },
                    Run = { LaunchCount = 1 ,WarmupCount=1,IterationCount=10},
                    Accuracy = { MaxRelativeError = 0.01 }
                });
            }
        }
    }


}
