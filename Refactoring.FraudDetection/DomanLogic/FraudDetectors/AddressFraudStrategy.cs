using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic.Entities;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic.FraudDetectors
{
    public sealed class AddressFraudStrategy : IFraudStrategy
    {
        public bool IsFraudulent(Order sample, Order target) =>
            sample.DealId == target.DealId
            && sample.State == target.State
            && sample.ZipCode == target.ZipCode
            && sample.Street == target.Street
            && sample.City == target.City
            && sample.CreditCard != target.CreditCard;
    }
}
