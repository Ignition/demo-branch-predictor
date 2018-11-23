using System;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace dotnet_testing
{
    public class BranchPredictor
    {
        private int[] data_unsorted;
        private int[] data_sorted;

        [GlobalSetup]
        public void Setup()
        {
            var rng = new Random(0);
            data_unsorted = Enumerable
                .Repeat(0, 10_000)
                .Select( i => rng.Next(0, 255) )
                .ToArray();

            data_sorted = new int[10_000];
            Array.Copy(data_unsorted, data_sorted, 10_000);
            Array.Sort(data_sorted);            

            //data_sorted = data_unsorted
            //    .OrderBy(x => x)
            //    .ToArray();
        }

        [Benchmark]
        public int Sorted() => data_sorted
                .Where( x => x >= 128 )
                .Sum();

        [Benchmark]
        public int Unsorted() => data_unsorted
                .Where( x => x >= 128 )
                .Sum();
    }

    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<BranchPredictor>();
        }
    }
}
