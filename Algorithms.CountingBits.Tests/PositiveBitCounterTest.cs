// -----------------------------------------------------------------------
// <copyright file="PositiveBitCounterTest.cs" company="Payvision">
//     Payvision Copyright © 2017
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Payvision.CodeChallenge.Algorithms.CountingBits.Tests
{
    [TestClass]
    public class PositiveBitCounterTest
    {
        private readonly IPositiveBitCounter[] _bitCounters =
        {
            new SimplePositiveBitCounter(),
            new SuperBitwisePositiveBitCounter(),
            new AlternativeBitwisePositiveBitCounter(),
            new BitwisePositiveBitCounter()
        };

        [TestMethod]
        public void Count_NegativeValue_ArgumentExceptionExpected()
        {
            foreach (var counter in _bitCounters)
            {
                try
                {
                    counter.Count(-2);
                }
                catch (ArgumentException)
                {
                    continue;
                }

                Assert.Fail("Should not get here.");
            }
        }

        [TestMethod]
        public void Count_Zero_NoOccurrences()
        {
            foreach (var counter in _bitCounters)
            {
                CollectionAssert.AreEqual(
                    new List<int> { 0 },
                    counter.Count(0).ToList(),
                    "The result is not the expected");
            }
        }

        [TestMethod]
        public void Count_ValidInput_OneOcurrence()
        {
            foreach (var counter in _bitCounters)
            {
                CollectionAssert.AreEqual(
                    new List<int> { 1, 0 },
                    counter.Count(1).ToList(),
                    "The result is not the expected");
            }
        }

        [TestMethod]
        public void Count_ValidInput_MultipleOcurrence()
        {
            foreach (var counter in _bitCounters)
            {
                CollectionAssert.AreEqual(
                    new List<int> { 3, 0, 5, 7 },
                    counter.Count(161).ToList(),
                    "The result is not the expected");
            }
        }

        [TestMethod]
        public void Count_ValidInput_MaxInt()
        {
            const int maxCapacity = sizeof(int) * 8;
            var expected = new List<int>(maxCapacity) { maxCapacity - 1 };
            for (var i = 0; i < expected[0]; i++)
            {
                expected.Add(i);
            }

            foreach (var counter in _bitCounters)
            {
                var actual = counter.Count(int.MaxValue).ToArray();

                CollectionAssert.AreEqual(
                    expected,
                    actual,
                    "The result is not the expected");
            }
        }
    }
}
