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
    /// Based on bitwise operations
    /// </summary>
    public sealed class BitwisePositiveBitCounter : IPositiveBitCounter
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
            var positions = new List<int>(maxCapacity);
            var current = input;
            var counter = 0;

            while (current != 0)
            {
                var bit = current & 1;
                if (bit == 1)
                {
                    positions.Add(counter);
                }

                current >>= 1;
                counter++;
            }

            yield return positions.Count;

            for (var i = 0; i < positions.Count; i++)
            {
                yield return positions[i];
            }
        }
    }
}
