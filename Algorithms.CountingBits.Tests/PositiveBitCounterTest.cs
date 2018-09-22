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
        private readonly PositiveBitCounter _bitCounter = new PositiveBitCounter();

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
    }
}
