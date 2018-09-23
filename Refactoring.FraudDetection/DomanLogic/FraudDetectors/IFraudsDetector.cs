using System.Collections.Generic;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic.Entities;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic.FraudDetectors
{
    public interface IFraudsDetector
    {
        IEnumerable<FraudResult> CheckOrders(IReadOnlyList<Order> orders);
    }
}
