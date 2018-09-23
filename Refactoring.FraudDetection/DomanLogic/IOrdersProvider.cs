using System.Collections.Generic;
using System.IO;
using Payvision.CodeChallenge.Refactoring.FraudDetection.Models;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic
{
    public interface IOrdersProvider
    {
        IEnumerable<Order> ReadOrders(StreamReader stream);
    }
}
