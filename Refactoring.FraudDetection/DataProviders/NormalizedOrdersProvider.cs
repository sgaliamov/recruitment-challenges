using System;
using System.IO;
using System.Linq;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic;
using Payvision.CodeChallenge.Refactoring.FraudDetection.Models;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DataProviders
{
    public sealed class NormalizedOrdersProvider : IOrdersProvider
    {
        private readonly IOrderNormalizer _normalizer;
        private readonly IOrdersProvider _provider;

        public NormalizedOrdersProvider(
            IOrdersProvider provider,
            IOrderNormalizer normalizer)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            _normalizer = normalizer ?? throw new ArgumentNullException(nameof(normalizer));
        }

        public Order[] ReadOrders(StreamReader reader) => _provider
            .ReadOrders(reader)
            .Select(_normalizer.Normalize)
            .ToArray();
    }
}
