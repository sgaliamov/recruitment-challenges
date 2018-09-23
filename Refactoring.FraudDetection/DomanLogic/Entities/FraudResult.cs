﻿namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic.Entities
{
    public sealed class FraudResult
    {
        public FraudResult(int orderId, bool isFraudulent)
        {
            OrderId = orderId;
            IsFraudulent = isFraudulent;
        }

        public int OrderId { get; }

        public bool IsFraudulent { get; }
    }
}
