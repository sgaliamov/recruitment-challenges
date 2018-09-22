using System.Collections.Generic;

namespace Payvision.CodeChallenge.Algorithms.CountingBits
{
    public interface IPositiveBitCounter
    {
        IEnumerable<int> Count(int input);
    }
}
