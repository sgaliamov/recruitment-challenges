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
        public void BenchmarkTest()
        {
            var random = new Random();
            var numbers = Enumerable.Range(0, 10000000).Select(_ => random.Next(int.MaxValue)).ToArray();

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            Run(numbers, new BitwisePositiveBitCounter()); // 3 times faster

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            Run(numbers, new SimplePositiveBitCounter());
        }

        private static void Run(
            IReadOnlyList<int> numbers,
            IPositiveBitCounter bitCounter)
        {
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
