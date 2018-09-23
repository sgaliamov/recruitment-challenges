using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic.Entities;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DataProviders.Normalizers
{
    public sealed class StateNormalizer : IOrderVisitor
    {
        public Order Visit(Order order) => new Order(order)
        {
            State = order.State
                .ToLowerInvariant()
                .Replace("il", "illinois")
                .Replace("ca", "california")
                .Replace("ny", "new york")
        };
    }
}
