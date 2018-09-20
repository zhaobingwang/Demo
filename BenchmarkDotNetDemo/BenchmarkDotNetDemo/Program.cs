using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BenchmarkDotNetDemo
{
    public class FakeClass
    {
        private readonly List<string> _fakeData = new List<string>();
        private readonly int _count = 1000000;
        private readonly string _needle = "needle ";
        public FakeClass()
        {
            Enumerable.Range(1, _count).ToList().ForEach(x => _fakeData.Add(x.ToString()));
            _fakeData.Insert(_count / 2, _needle);
        }
        [Benchmark]
        public string Single() => _fakeData.SingleOrDefault(x => x == _needle);
        [Benchmark]
        public string First() => _fakeData.FirstOrDefault(x => x == _needle);

        public List<string> Get()
        {
            return _fakeData;
        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<FakeClass>();
            Console.Read();
        }
    }
}
