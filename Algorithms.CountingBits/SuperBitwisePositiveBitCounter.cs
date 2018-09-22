// -----------------------------------------------------------------------
// <copyright file="BitCounter.cs" company="Payvision">
//     Payvision Copyright © 2017
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Payvision.CodeChallenge.Algorithms.CountingBits
{
    /// <summary>
    ///     Super optimized bitwise counter
    /// </summary>
    public sealed class SuperBitwisePositiveBitCounter : IPositiveBitCounter
    {
        public IEnumerable<int> Count(int input)
        {
            if (input < 0)
            {
                throw new ArgumentException("Parameter must be a positive integer.", nameof(input));
            }

            return GetCount(input);
        }

        [DllImport(
            @"..\..\..\Algorithms.CountingBits.Impl\Release\Algorithms.CountingBits.Impl.dll",
            CallingConvention = CallingConvention.Cdecl)]
        private static extern void Count(int input, out ArrayStruct result);

        private static IEnumerable<int> GetCount(int input)
        {
            Count(input, out var result);

            for (var i = 0; i < result.Length; i++)
            {
                yield return result.Data[i];
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ArrayStruct
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public int[] Data;

            public int Length;
        }
    }
}
