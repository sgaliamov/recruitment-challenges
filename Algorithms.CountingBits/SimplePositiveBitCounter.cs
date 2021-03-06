﻿// -----------------------------------------------------------------------
// <copyright file="BitCounter.cs" company="Payvision">
//     Payvision Copyright © 2017
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

namespace Payvision.CodeChallenge.Algorithms.CountingBits
{
    /// <summary>
    ///     Straightforward implementation
    /// </summary>
    public sealed class SimplePositiveBitCounter : IPositiveBitCounter
    {
        public IEnumerable<int> Count(int input)
        {
            if (input < 0)
            {
                throw new ArgumentException("Parameter must be a positive integer.", nameof(input));
            }

            return Count(new BitArray(new[] { input }));
        }

        private static IEnumerable<int> Count(BitArray bitArray)
        {
            var count = 0;
            for (var i = 0; i < bitArray.Length; i++)
            {
                if (bitArray[i])
                {
                    count++;
                }
            }

            yield return count;

            for (var i = 0; i < bitArray.Length; i++)
            {
                if (bitArray[i])
                {
                    yield return i;
                }
            }
        }
    }
}
