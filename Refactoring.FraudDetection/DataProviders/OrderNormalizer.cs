using System;
using Payvision.CodeChallenge.Refactoring.FraudDetection.Models;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DataProviders
{
    public sealed class OrderNormalizer : IOrderNormalizer
    {
        // todo: clone order, apply strategies, introduce constants, email normalization
        public Order Normalize(Order order)
        {
            //Normalize email
            var aux = order.Email.Split(new[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            order.Email = string.Join("@", aux[0], aux[1]);

            //Normalize street
            order.Street = order.Street.Replace("st.", "street").Replace("rd.", "road");

            //Normalize state
            order.State = order.State.Replace("il", "illinois")
                .Replace("ca", "california")
                .Replace("ny", "new york");

            return order;
        }
    }
}
