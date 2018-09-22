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
        private const int M1 = 0x55555555; //binary: 0101...
        private const int M2 = 0x33333333; //binary: 00110011..
        private const int M4 = 0x0f0f0f0f; //binary:  4 zeros,  4 ones ...

        public IEnumerable<int> Count(int input)
        {
            if (input < 0)
            {
                throw new ArgumentException("Parameter must be a positive integer.", nameof(input));
            }

            return GetCountData(input);
        }

        private static int GetBitCount(int input)
        {
            input -= (input >> 1) & M1; //put count of each 2 bits into those 2 bits
            input = (input & M2) + ((input >> 2) & M2); //put count of each 4 bits into those 4 bits
            input = (input + (input >> 4)) & M4; //put count of each 8 bits into those 8 bits
            input += input >> 8; //put count of each 16 bits into their lowest 8 bits
            input += input >> 16; //put count of each 32 bits into their lowest 8 bits

            return input & 0x7f;
        }

        private static IEnumerable<int> GetCountData(int input)
        {
            yield return GetBitCount(input);

            foreach (var position in GetSwitchedBitPositions(input))
            {
                yield return position;
            }
        }

        private static IEnumerable<int> GetSwitchedBitPositions(int input)
        {
            var iterator = 0;

            for (var i = 1; i <= (0b01000000_00000000_00000000_00000000) && i > 0; i <<= 1, iterator++)
            {
                if ((input & i) > 0)
                {
                    yield return iterator;
                }
            }
        }
    }
}
