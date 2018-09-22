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
        private readonly IPositiveBitCounter _bitCounter = new SuperBitwisePositiveBitCounter();

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Count_NegativeValue_ArgumentExceptionExpected()
        {
            _bitCounter.Count(-2);
        }

        [TestMethod]
        public void Count_Zero_NoOccurrences()
        {
            CollectionAssert.AreEqual(
                new List<int> { 0 },
                _bitCounter.Count(0).ToList(),
                "The result is not the expected");
        }

        [TestMethod]
        public void Count_ValidInput_OneOcurrence()
        {
            CollectionAssert.AreEqual(
                new List<int> { 1, 0 },
                _bitCounter.Count(1).ToList(),
                "The result is not the expected");
        }

        [TestMethod]
        public void Count_ValidInput_MultipleOcurrence()
        {
            CollectionAssert.AreEqual(
                new List<int> { 3, 0, 5, 7 },
                _bitCounter.Count(161).ToList(),
                "The result is not the expected");
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

            var actual = _bitCounter.Count(int.MaxValue).ToArray();

            CollectionAssert.AreEqual(
                expected,
                actual,
                "The result is not the expected");
        }
    }
}
