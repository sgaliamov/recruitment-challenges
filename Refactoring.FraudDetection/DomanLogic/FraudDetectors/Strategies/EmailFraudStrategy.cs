using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic.Entities;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic.FraudDetectors.Strategies
{
    public sealed class EmailFraudStrategy : IFraudStrategy
    {
        public bool IsFraudulent(Order sample, Order target) =>
            sample.DealId == target.DealId
            && sample.Email == target.Email
            && sample.CreditCard != target.CreditCard;
    }
}
