using System.IO;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic.Entities;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic
{
    public interface IOrdersProvider
    {
        Order[] ReadOrders(StreamReader stream);
    }
}
