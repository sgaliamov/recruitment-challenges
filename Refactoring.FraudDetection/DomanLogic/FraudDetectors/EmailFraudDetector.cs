using Payvision.CodeChallenge.Refactoring.FraudDetection.Models;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic.FraudDetectors
{
    internal class EmailFraudDetector : IOrderFraudDetector
    {
        public bool IsFraudulent(Order source, Order target) =>
            source.DealId == target.DealId
            && source.Email == target.Email
            && source.CreditCard != target.CreditCard;
    }
}
