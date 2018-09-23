using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Payvision.CodeChallenge.Algorithms.CountingBits.Tests
{
    [TestClass]
    public class PositiveBitCounterBenchmarkTest
    {
        private int[] _numbers;

        [TestInitialize]
        public void Initialize()
        {
            var random = new Random();
            _numbers = Enumerable.Range(0, 50000000).Select(_ => random.Next(int.MaxValue)).ToArray();
        }

        [TestMethod]
        public void Benchmark_AlternativeBitwisePositiveBitCounter()
        {
            Run(_numbers, new AlternativeBitwisePositiveBitCounter());
        }

        [TestMethod]
        public void Benchmark_SuperBitwisePositiveBitCounter()
        {
            Run(_numbers, new SuperBitwisePositiveBitCounter()); // slightly faster than BitwisePositiveBitCounter
        }

        [TestMethod]
        public void Benchmark_BitwisePositiveBitCounter()
        {
            Run(_numbers, new BitwisePositiveBitCounter()); // 6 times faster
        }

        [TestMethod]
        public void Benchmark_SimplePositiveBitCounter()
        {
            Run(_numbers, new SimplePositiveBitCounter());
        }

        private static void Run(
            IReadOnlyList<int> numbers,
            IPositiveBitCounter bitCounter)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (var i = 0; i < numbers.Count; i++)
            {
                bitCounter.Count(numbers[i]);
            }

            stopwatch.Stop();

            Console.WriteLine($"{bitCounter.GetType().Name}: {stopwatch.ElapsedMilliseconds}");
        }
    }
}
