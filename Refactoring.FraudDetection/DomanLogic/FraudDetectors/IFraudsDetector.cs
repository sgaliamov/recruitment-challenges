using System.Collections.Generic;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic.ValueObjects;
using Payvision.CodeChallenge.Refactoring.FraudDetection.Models;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic.FraudDetectors
{
    // todo: tests
    public interface IFraudsDetector
    {
        IEnumerable<FraudResult> CheckOrders(IReadOnlyList<Order> orders);
    }
}
