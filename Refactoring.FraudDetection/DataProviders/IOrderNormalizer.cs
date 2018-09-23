using Payvision.CodeChallenge.Refactoring.FraudDetection.Models;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DataProviders
{
    public interface IOrderNormalizer
    {
        Order Normalize(Order order);
    }
}