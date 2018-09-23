using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic;
using Payvision.CodeChallenge.Refactoring.FraudDetection.Models;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DataProviders
{
    // todo: unit test, lower strings, error handling
    public sealed class OrdersProvider : IOrdersProvider
    {
        private readonly IOrderNormalizer _normalizer;

        public OrdersProvider(IOrderNormalizer normalizer) => _normalizer = normalizer;

        public IEnumerable<Order> ReadOrders(StreamReader reader)
        {
            using (var csv = new CsvReader(reader, true)
            {
                Configuration =
                {
                    HasHeaderRecord = false
                }
            })
            {
                var records = csv
                    .GetRecords<Order>()
                    .Select(_normalizer.Normalize);

                return records;
            }
        }
    }
}
