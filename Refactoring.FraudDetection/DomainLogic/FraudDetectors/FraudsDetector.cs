using System;
using System.Collections.Generic;
using System.Linq;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomainLogic.Entities;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomainLogic.FraudDetectors.Strategies;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DomainLogic.FraudDetectors
{
    public sealed class FraudsDetector : IFraudsDetector
    {
        private readonly IFraudStrategy[] _strategies;

        public FraudsDetector(IFraudStrategy[] strategies) =>
            _strategies = strategies ?? throw new ArgumentNullException(nameof(strategies));

        public IEnumerable<FraudResult> CheckOrders(IReadOnlyList<Order> orders)
        {
            if (orders == null)
            {
                throw new ArgumentNullException(nameof(orders));
            }

            var fraudResults = new List<FraudResult>();

            for (var i = 0; i < orders.Count - 1; i++)
            {
                var results = CheckFollowingOrders(orders, i);

                fraudResults.AddRange(results);
            }

            return fraudResults;
        }

        private IEnumerable<FraudResult> CheckFollowingOrders(IReadOnlyList<Order> orders, int start)
        {
            var sample = orders[start];

            for (var j = start + 1; j < orders.Count; j++)
            {
                var target = orders[j];

                var isFraudulent = _strategies
                    .Select(strategy => strategy.IsFraudulent(sample, target))
                    .Any(x => x);

                if (isFraudulent)
                {
                    yield return new FraudResult(target.OrderId, true);
                }
            }
        }
    }
}
