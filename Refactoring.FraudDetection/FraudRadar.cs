// -----------------------------------------------------------------------
// <copyright file="FraudRadar.cs" company="Payvision">
//     Payvision Copyright © 2017
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic.ValueObjects;
using Payvision.CodeChallenge.Refactoring.FraudDetection.Models;
using Payvision.CodeChallenge.Refactoring.FraudDetection.Shared;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection
{
    // todo: check input arguments everywhere, unit test, error logger
    public sealed class FraudRadar
    {
        private readonly IStructuredLogger _logger;
        private readonly IOrdersProvider _ordersProvider;

        public FraudRadar(IStructuredLogger logger, IOrdersProvider ordersProvider)
        {
            _logger = logger
                      ?? throw new ArgumentNullException(nameof(_logger));

            _ordersProvider = ordersProvider
                              ?? throw new ArgumentNullException(nameof(ordersProvider));
        }

        public IEnumerable<FraudResult> Check(StreamReader reader)
        {
            var orders = _ordersProvider.ReadOrders(reader).ToArray();

            return CheckOrders(orders);
        }

        private static IEnumerable<FraudResult> CheckOrders(IReadOnlyList<Order> orders)
        {
            var fraudResults = new List<FraudResult>();

            for (var i = 0; i < orders.Count; i++)
            {
                var source = orders[i];

                for (var j = i + 1; j < orders.Count; j++)
                {
                    var isFraudulent = false;

                    var target = orders[j];

                    if (source.DealId == target.DealId
                        && source.Email == target.Email
                        && source.CreditCard != target.CreditCard)
                    {
                        isFraudulent = true;
                    }

                    if (source.DealId == target.DealId
                        && source.State == target.State
                        && source.ZipCode == target.ZipCode
                        && source.Street == target.Street
                        && source.City == target.City
                        && source.CreditCard != target.CreditCard)
                    {
                        isFraudulent = true;
                    }

                    if (isFraudulent)
                    {
                        fraudResults.Add(new FraudResult(target.OrderId, true));
                    }
                }
            }

            return fraudResults;
        }
    }
}
