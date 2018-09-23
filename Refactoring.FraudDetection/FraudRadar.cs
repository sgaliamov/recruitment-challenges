// -----------------------------------------------------------------------
// <copyright file="FraudRadar.cs" company="Payvision">
//     Payvision Copyright © 2017
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection
{
    public class FraudRadar
    {
        public IEnumerable<FraudResult> Check(string filePath)
        {
            var orders = ReadOrders(filePath);

            Normalize(orders);

            return CheckOrders(orders);
        }

        private static IEnumerable<FraudResult> CheckOrders(List<Order> orders)
        {
            var fraudResults = new List<FraudResult>();

            for (var i = 0; i < orders.Count; i++)
            {
                var current = orders[i];

                for (var j = i + 1; j < orders.Count; j++)
                {
                    var isFraudulent = false;

                    if (current.DealId == orders[j].DealId
                        && current.Email == orders[j].Email
                        && current.CreditCard != orders[j].CreditCard)
                    {
                        isFraudulent = true;
                    }

                    if (current.DealId == orders[j].DealId
                        && current.State == orders[j].State
                        && current.ZipCode == orders[j].ZipCode
                        && current.Street == orders[j].Street
                        && current.City == orders[j].City
                        && current.CreditCard != orders[j].CreditCard)
                    {
                        isFraudulent = true;
                    }

                    if (isFraudulent)
                    {
                        fraudResults.Add(new FraudResult { IsFraudulent = true, OrderId = orders[j].OrderId });
                    }
                }
            }

            return fraudResults;
        }

        private static void Normalize(List<Order> orders)
        {
            // NORMALIZE
            foreach (var order in orders)
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
            }
        }

        private static List<Order> ReadOrders(string filePath)
        {
            // READ FRAUD LINES
            var orders = new List<Order>();

            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                var items = line.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                var order = new Order
                {
                    OrderId = int.Parse(items[0]),
                    DealId = int.Parse(items[1]),
                    Email = items[2].ToLower(),
                    Street = items[3].ToLower(),
                    City = items[4].ToLower(),
                    State = items[5].ToLower(),
                    ZipCode = items[6],
                    CreditCard = items[7]
                };

                orders.Add(order);
            }

            return orders;
        }
    }
}
