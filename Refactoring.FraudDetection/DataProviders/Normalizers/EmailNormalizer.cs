using System;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomainLogic;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomainLogic.Entities;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DataProviders.Normalizers
{
    public sealed class EmailNormalizer : IOrderVisitor
    {
        public Order Visit(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            if (order.Email == null)
            {
                return order;
            }

            return new Order(order)
            {
                Email = NormalizeEmail(order.Email)
            };
        }

        private static string NormalizeEmail(string email)
        {
            var aux = email
                .Trim()
                .ToLowerInvariant()
                .Split(new[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            if (aux.Length != 2)
            {
                throw new NormalizationException($"Email {email} is not valid.");
            }

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0
                ? aux[0].Replace(".", "")
                : aux[0].Remove(atIndex).Replace(".", ""); // here was a bug

            return string.Join("@", aux[0], aux[1]);
        }
    }
}
