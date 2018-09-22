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
        [TestMethod]
        public void RunBenchmark()
        {
            var random = new Random();
            var numbers = Enumerable.Range(0, 50000000).Select(_ => random.Next(int.MaxValue)).ToArray();

            Run(numbers, new BitwisePositiveBitCounter()); // 6 times faster
            Run(numbers, new SimplePositiveBitCounter());
            Run(numbers, new OptimizedBitwisePositiveBitCounter());
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
