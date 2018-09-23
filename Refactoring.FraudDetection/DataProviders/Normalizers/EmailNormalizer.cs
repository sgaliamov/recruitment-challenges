using System;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic.Entities;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DataProviders.Normalizers
{
    public sealed class EmailNormalizer : IOrderVisitor
    {
        public Order Visit(Order order) => new Order(order)
        {
            Email = NormalizeEmail(order.Email)
        };

        private static string NormalizeEmail(string email)
        {
            var aux = email
                .ToLowerInvariant()
                .Split(new[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            return string.Join("@", aux[0], aux[1]);
        }
    }
}
