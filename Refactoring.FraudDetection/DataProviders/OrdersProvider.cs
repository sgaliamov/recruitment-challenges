using System;
using System.IO;
using System.Linq;
using CsvHelper;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic.Entities;
using Payvision.CodeChallenge.Refactoring.FraudDetection.Shared;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DataProviders
{
    public sealed class OrdersProvider : IOrdersProvider
    {
        private readonly IStructuredLogger _logger;

        public OrdersProvider(IStructuredLogger logger) =>
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public Order[] ReadOrders(StreamReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            using (var csv = new CsvReader(reader, true)
            {
                Configuration =
                {
                    HasHeaderRecord = false,
                    BadDataFound = OnError
                }
            })
            {
                return csv.GetRecords<Order>().ToArray();
            }
        }

        private void OnError(ReadingContext context)
        {
            _logger.Error("Bad data found: {Record}", context.RawRecord);

            throw new InvalidDataException($"Bad data found: {context.RawRecord}");
        }
    }
}
