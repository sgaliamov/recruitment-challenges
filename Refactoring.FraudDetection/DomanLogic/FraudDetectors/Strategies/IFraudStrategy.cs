using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic.Entities;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic.FraudDetectors.Strategies
{
    public interface IFraudStrategy
    {
        bool IsFraudulent(Order sample, Order target);
    }
}
