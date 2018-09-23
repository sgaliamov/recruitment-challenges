using System;
using System.Collections.Generic;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic.Entities;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DataProviders.Normalizers
{
    public sealed class StreetNormalizer : IOrderVisitor
    {
        private readonly IReadOnlyDictionary<string, string> _replacements;

        public StreetNormalizer(IReadOnlyDictionary<string, string> replacements) =>
            _replacements = replacements ?? throw new ArgumentNullException(nameof(replacements));

        public Order Visit(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            if (order.Street == null)
            {
                return order;
            }

            return new Order(order)
            {
                Street = NormalizeStreet(order.Street)
            };
        }

        private string NormalizeStreet(string street)
        {
            street = street
                .ToLowerInvariant()
                .Trim();

            if (_replacements.ContainsKey(street))
            {
                return _replacements[street]; // unsafe replacement was used
            }

            return street;
        }
    }
}
