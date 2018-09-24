using System;
using System.Collections.Generic;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomainLogic;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomainLogic.Entities;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DataProviders.Normalizers
{
    public sealed class StateNormalizer : IOrderVisitor
    {
        private readonly IReadOnlyDictionary<string, string> _replacements;

        public StateNormalizer(IReadOnlyDictionary<string, string> replacements) => 
            _replacements = replacements ?? throw new ArgumentNullException(nameof(replacements));

        public Order Visit(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            if (order.State == null)
            {
                return order;
            }

            return new Order(order)
            {
                State = NormalizeState(order.State)
            };
        }

        private string NormalizeState(string state)
        {
            state = state
                .ToLowerInvariant()
                .Trim();

            if (_replacements.ContainsKey(state))
            {
                return _replacements[state]; // unsafe replacement was used
            }

            return state;
        }
    }
}
