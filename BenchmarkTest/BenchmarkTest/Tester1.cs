using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace BenchmarkTest
{
    [MemoryDiagnoser]
    public class Tester1
    {
        [Benchmark]
        [IterationCount(3)]
        [Arguments(3)]
        public void Run(int max)
        {
            for (int i = 0; i < max; i++)
            {
                Console.WriteLine(i);
            }
        }

    }
}
