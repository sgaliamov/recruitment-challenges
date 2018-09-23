using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic.Entities;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DataProviders.Normalizers
{
    public sealed class StreetNormalizer : IOrderVisitor
    {
        public Order Visit(Order order) => new Order(order)
        {
            Street = order.Street
                .ToLowerInvariant()
                .Replace("st.", "street")
                .Replace("rd.", "road")
        };
    }
}
