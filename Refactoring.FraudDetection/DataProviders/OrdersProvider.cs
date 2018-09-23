using System;
using System.IO;
using System.Linq;
using CsvHelper;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic;
using Payvision.CodeChallenge.Refactoring.FraudDetection.Models;
using Payvision.CodeChallenge.Refactoring.FraudDetection.Shared;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DataProviders
{
    public sealed class OrdersProvider : IOrdersProvider
    {
        private readonly IStructuredLogger _logger;
        private readonly IOrderNormalizer _normalizer;

        public OrdersProvider(
            IStructuredLogger logger,
            IOrderNormalizer normalizer)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _normalizer = normalizer ?? throw new ArgumentNullException(nameof(normalizer));
        }

        public Order[] ReadOrders(StreamReader reader)
        {
            using (var csv = new CsvReader(reader, true)
            {
                Configuration =
                {
                    HasHeaderRecord = false,
                    BadDataFound = OnError
                }
            })
            {
                return csv
                    .GetRecords<Order>()
                    .Select(_normalizer.Normalize)
                    .ToArray();
            }
        }

        private void OnError(ReadingContext context)
        {
            _logger.Error("Bad data found: {Record}", context.RawRecord);

            throw new InvalidOperationException($"Bad data found: {context.RawRecord}");
        }
    }
}
