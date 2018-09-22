// -----------------------------------------------------------------------
// <copyright file="BitCounter.cs" company="Payvision">
//     Payvision Copyright © 2017
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Payvision.CodeChallenge.Algorithms.CountingBits
{
    /// <summary>
    ///     Optimized bitwise counter
    /// </summary>
    public sealed class OptimizedBitwisePositiveBitCounter : IPositiveBitCounter
    {
        public IEnumerable<int> Count(int input)
        {
            if (input < 0)
            {
                throw new ArgumentException(nameof(input));
            }

            return GetCount(input);
        }

        private static IEnumerable<int> GetCount(int input)
        {
            const int maxCapacity = sizeof(int) * 8 - 1;
            var positions = new Stack<int>(maxCapacity);

            var index = 1;
            var marker = 0b01000000_00000000_00000000_00000000;

            while (marker != 0)
            {
                var bit = input & marker;
                if (bit > 0)
                {
                    positions.Push(index);
                }

                marker >>= 1;
                index++;
            }

            yield return positions.Count;

            while (positions.Count != 0)
            {
                yield return maxCapacity - positions.Pop();
            }
        }
    }
}
