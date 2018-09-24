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
    ///     Based on bitwise operations.
    ///     Not thread safe.
    /// </summary>
    public sealed class BitwisePositiveBitCounter : IPositiveBitCounter
    {
        private const int MaxCapacity = sizeof(int) * 8 - 1;
        private static readonly int[] Positions = new int[MaxCapacity];

        public IEnumerable<int> Count(int input)
        {
            if (input < 0)
            {
                throw new ArgumentException("Parameter must be a positive integer.", nameof(input));
            }

            return GetCount(input);
        }

        private static IEnumerable<int> GetCount(int input)
        {
            var counter = 0;
            var index = 0;
            
            while (input != 0)
            {
                if ((input & 1) == 1)
                {
                    Positions[counter++] = index;
                }

                input >>= 1;
                index++;
            }

            yield return counter;

            for (var i = 0; i < counter; i++)
            {
                yield return Positions[i];
            }
        }
    }
}
