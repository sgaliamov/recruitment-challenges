// -----------------------------------------------------------------------
// <copyright file="FraudRadar.cs" company="Payvision">
//     Payvision Copyright © 2017
// </copyright>
// -----------------------------------------------------------------------

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic.Entities
{
    public sealed class Order
    {
        // ReSharper disable once UnusedMember.Global
        public Order() { }

        public Order(Order order)
        {
            OrderId = order.OrderId;
            DealId = order.DealId;
            Email = order.Email;
            Street = order.Street;
            City = order.City;
            State = order.State;
            ZipCode = order.ZipCode;
            CreditCard = order.CreditCard;
        }

        public int OrderId { get; set; }

        public int DealId { get; set; }

        public string Email { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string CreditCard { get; set; }

        public Order Apply(IOrderVisitor visitor) => visitor.Visit(this);
    }
}
