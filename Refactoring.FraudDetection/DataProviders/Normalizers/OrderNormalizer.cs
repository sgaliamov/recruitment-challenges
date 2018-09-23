using System.Linq;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic.Entities;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DataProviders.Normalizers
{
    public sealed class OrderNormalizer : IOrderVisitor
    {
        private readonly IOrderVisitor[] _normalizers;

        public OrderNormalizer(IOrderVisitor[] normalizers) => _normalizers = normalizers;

        public Order Visit(Order order) => _normalizers.Aggregate(order, (o, visitor) => o.Apply(visitor));
    }
}
