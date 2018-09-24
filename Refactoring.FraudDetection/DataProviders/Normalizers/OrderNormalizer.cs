using System;
using System.Linq;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomainLogic;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomainLogic.Entities;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DataProviders.Normalizers
{
    public sealed class OrderNormalizer : IOrderVisitor
    {
        private readonly IOrderVisitor[] _normalizers;

        public OrderNormalizer(IOrderVisitor[] normalizers) =>
            _normalizers = normalizers ?? throw new ArgumentNullException(nameof(normalizers));

        public Order Visit(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            return _normalizers.Aggregate(order, (o, visitor) => o.Apply(visitor));
        }
    }
}
