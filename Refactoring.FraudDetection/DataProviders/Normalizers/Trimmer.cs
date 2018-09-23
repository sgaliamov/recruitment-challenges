using System;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic.Entities;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DataProviders.Normalizers
{
    public sealed class Trimmer : IOrderVisitor
    {
        private readonly Func<Order, string> _getField;
        private readonly Action<Order, string> _setField;

        public Trimmer(
            Func<Order, string> getField,
            Action<Order, string> setField)
        {
            _getField = getField ?? throw new ArgumentNullException(nameof(getField));
            _setField = setField ?? throw new ArgumentNullException(nameof(setField));
        }

        public Order Visit(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            var value = _getField(order);
            if (value == null)
            {
                return order;
            }

            var clone = new Order(order);

            _setField(clone, value.ToLowerInvariant().Trim());

            return clone;
        }
    }
}
