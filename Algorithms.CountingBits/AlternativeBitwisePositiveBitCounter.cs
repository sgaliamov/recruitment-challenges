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
    ///     Alternative bitwise counter
    /// </summary>
    public sealed class AlternativeBitwisePositiveBitCounter : IPositiveBitCounter
    {
        private const int M1 = 0x55555555;
        private const int M2 = 0x33333333;
        private const int M4 = 0x0f0f0f0f;

        public IEnumerable<int> Count(int input)
        {
            if (input < 0)
            {
                throw new ArgumentException("Parameter must be a positive integer.", nameof(input));
            }

            return GetCount(input);
        }

        private static int GetBitCount(int input)
        {
            input -= (input >> 1) & M1;
            input = (input & M2) + ((input >> 2) & M2);
            input = (input + (input >> 4)) & M4;
            input += input >> 8;
            input += input >> 16;

            return input & 0x7f;
        }

        private static IEnumerable<int> GetCount(int input)
        {
            yield return GetBitCount(input);

            for (var index = 0; input != 0; input >>= 1, index++)
            {
                if ((input & 1) == 1)
                {
                    yield return index;
                }
            }
        }
    }
}
