// -----------------------------------------------------------------------
// <copyright file="FraudRadar.cs" company="Payvision">
//     Payvision Copyright © 2017
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic.FraudDetectors;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic.ValueObjects;
using Payvision.CodeChallenge.Refactoring.FraudDetection.Shared;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection
{
    // todo: check input arguments everywhere, unit test, error logger
    public sealed class FraudRadar
    {
        private readonly IFraudsDetector _detector;
        private readonly IStructuredLogger _logger;
        private readonly IOrdersProvider _ordersProvider;

        public FraudRadar(
            IStructuredLogger logger,
            IOrdersProvider ordersProvider,
            IFraudsDetector detector)
        {
            _detector = detector ?? throw new ArgumentNullException(nameof(detector));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _ordersProvider = ordersProvider ?? throw new ArgumentNullException(nameof(ordersProvider));
        }

        public IEnumerable<FraudResult> Check(StreamReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            var orders = _ordersProvider.ReadOrders(reader);

            // ReSharper disable once CoVariantArrayConversion
            _logger.Debug("Read orders: {Orders}", orders);

            var fraudResults = _detector.CheckOrders(orders);

            _logger.Debug("Frauds: {Frauds}", fraudResults);

            return fraudResults;
        }
    }
}
