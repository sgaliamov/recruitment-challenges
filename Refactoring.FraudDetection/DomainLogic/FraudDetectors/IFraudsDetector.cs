using System.Collections.Generic;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomainLogic.Entities;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DomainLogic.FraudDetectors
{
    public interface IFraudsDetector
    {
        IEnumerable<FraudResult> CheckOrders(IReadOnlyList<Order> orders);
    }
}
