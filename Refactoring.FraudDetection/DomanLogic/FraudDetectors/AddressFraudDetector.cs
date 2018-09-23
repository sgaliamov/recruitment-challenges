using Payvision.CodeChallenge.Refactoring.FraudDetection.Models;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic.FraudDetectors
{
    internal class AddressFraudDetector : IOrderFraudDetector
    {
        public bool IsFraudulent(Order source, Order target) =>
            source.DealId == target.DealId
            && source.State == target.State
            && source.ZipCode == target.ZipCode
            && source.Street == target.Street
            && source.City == target.City
            && source.CreditCard != target.CreditCard;
    }
}
