using System.IO;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomainLogic.Entities;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DomainLogic
{
    public interface IOrdersProvider
    {
        Order[] ReadOrders(StreamReader stream);
    }
}
