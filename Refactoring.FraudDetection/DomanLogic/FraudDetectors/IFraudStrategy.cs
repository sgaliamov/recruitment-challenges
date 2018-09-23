using Payvision.CodeChallenge.Refactoring.FraudDetection.Models;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic
{
    public interface IFraudStrategy
    {
        bool IsFraudulent(Order sample, Order target);
    }
}
