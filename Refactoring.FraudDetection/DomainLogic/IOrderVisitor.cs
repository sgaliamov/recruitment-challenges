using Payvision.CodeChallenge.Refactoring.FraudDetection.DomainLogic.Entities;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DomainLogic
{
    public interface IOrderVisitor
    {
        Order Visit(Order order);
    }
}
