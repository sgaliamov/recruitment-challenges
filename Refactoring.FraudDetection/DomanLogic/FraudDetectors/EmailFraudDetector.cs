using Payvision.CodeChallenge.Refactoring.FraudDetection.Models;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic.FraudDetectors
{
    public sealed class EmailFraudStrategy : IFraudStrategy
    {
        public bool IsFraudulent(Order sample, Order target) =>
            sample.DealId == target.DealId
            && sample.Email == target.Email
            && sample.CreditCard != target.CreditCard;
    }
}
