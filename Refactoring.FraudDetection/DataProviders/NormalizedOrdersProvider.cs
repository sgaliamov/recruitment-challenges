using System;
using System.IO;
using System.Linq;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DataProviders.Normalizers;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic.Entities;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DataProviders
{
    /// <summary>
    ///     Normalization decorator
    /// </summary>
    public sealed class NormalizedOrdersProvider : IOrdersProvider
    {
        private readonly IOrderVisitor _normalizer;
        private readonly IOrdersProvider _provider;

        public NormalizedOrdersProvider(
            IOrdersProvider provider,
            IOrderVisitor normalizer)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            _normalizer = normalizer ?? throw new ArgumentNullException(nameof(normalizer));
        }

        public Order[] ReadOrders(StreamReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            return _provider
                .ReadOrders(reader)
                .Select(_normalizer.Visit)
                .ToArray();
        }
    }
}
