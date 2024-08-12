// See https://aka.ms/new-console-template for more information





using BenchmarkDotNet.Running;
using BenchmarkTest;

//var summary = BenchmarkRunner.Run<Tester1>();

//var summary = BenchmarkRunner.Run(typeof(Program).Assembly);
var summary = BenchmarkRunner.Run(typeof(Md5VsSha256));


Console.ReadLine();
