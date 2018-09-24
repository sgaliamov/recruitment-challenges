using Payvision.CodeChallenge.Refactoring.FraudDetection.DomainLogic.Entities;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DomainLogic.FraudDetectors.Strategies
{
    public interface IFraudStrategy
    {
        bool IsFraudulent(Order sample, Order target);
    }
}
