using System.IO;
using Payvision.CodeChallenge.Refactoring.FraudDetection.Models;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic
{
    public interface IOrdersProvider
    {
        Order[] ReadOrders(StreamReader stream);
    }
}
