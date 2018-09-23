using Payvision.CodeChallenge.Refactoring.FraudDetection.Models;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic
{
    public interface IOrderFraudDetector
    {
        bool IsFraudulent(Order source, Order target);
    }
}
