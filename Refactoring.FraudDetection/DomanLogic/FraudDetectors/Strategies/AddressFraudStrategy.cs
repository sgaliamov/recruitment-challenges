using System;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic.Entities;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic.FraudDetectors.Strategies
{
    public sealed class AddressFraudStrategy : IFraudStrategy
    {
        public bool IsFraudulent(Order sample, Order target)
        {
            if (sample == null)
            {
                throw new ArgumentNullException(nameof(sample));
            }

            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            return sample.DealId == target.DealId
                   && sample.State == target.State
                   && sample.ZipCode == target.ZipCode
                   && sample.Street == target.Street
                   && sample.City == target.City
                   && sample.CreditCard != target.CreditCard;
        }
    }
}
