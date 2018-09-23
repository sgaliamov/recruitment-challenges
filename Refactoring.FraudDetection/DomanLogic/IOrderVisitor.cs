using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic.Entities;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic
{
    public interface IOrderVisitor
    {
        Order Visit(Order order);
    }
}
