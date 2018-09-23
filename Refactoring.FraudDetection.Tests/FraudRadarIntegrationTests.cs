// -----------------------------------------------------------------------
// <copyright file="FraudRadarTests.cs" company="Payvision">
//     Payvision Copyright © 2017
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DataProviders;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DataProviders.Normalizers;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic.Entities;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic.FraudDetectors;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic.FraudDetectors.Strategies;
using Payvision.CodeChallenge.Refactoring.FraudDetection.Shared;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.Tests
{
    [TestClass]
    public class FraudRadarIntegrationTests
    {
        [TestMethod]
        [DeploymentItem("./Files/OneLineFile.txt", "Files")]
        public void CheckFraud_OneLineFile_NoFraudExpected()
        {
            var result = ExecuteTest(Path.Combine(Environment.CurrentDirectory, "Files", "OneLineFile.txt"));

            result.Should().NotBeNull("The result should not be null.");
            result.Count.Should().Be(0, "The result should not contains fraudulent lines");
        }

        [TestMethod]
        [DeploymentItem("./Files/TwoLines_FraudulentSecond.txt", "Files")]
        public void CheckFraud_TwoLines_SecondLineFraudulent()
        {
            var result = ExecuteTest(Path.Combine(
                Environment.CurrentDirectory,
                "Files",
                "TwoLines_FraudulentSecond.txt"));

            result.Should().NotBeNull("The result should not be null.");
            result.Count.Should().Be(1, "The result should contains the number of lines of the file");
            result.First().IsFraudulent.Should().BeTrue("The first line is not fraudulent");
            result.First().OrderId.Should().Be(2, "The first line is not fraudulent");
        }

        [TestMethod]
        [DeploymentItem("./Files/ThreeLines_FraudulentSecond.txt", "Files")]
        public void CheckFraud_ThreeLines_SecondLineFraudulent()
        {
            var result = ExecuteTest(Path.Combine(
                Environment.CurrentDirectory,
                "Files",
                "ThreeLines_FraudulentSecond.txt"));

            result.Should().NotBeNull("The result should not be null.");
            result.Count.Should().Be(1, "The result should contains the number of lines of the file");
            result.First().IsFraudulent.Should().BeTrue("The first line is not fraudulent");
            result.First().OrderId.Should().Be(2, "The first line is not fraudulent");
        }

        [TestMethod]
        [DeploymentItem("./Files/FourLines_MoreThanOneFraudulent.txt", "Files")]
        public void CheckFraud_FourLines_MoreThanOneFraudulent()
        {
            var result = ExecuteTest(Path.Combine(
                Environment.CurrentDirectory,
                "Files",
                "FourLines_MoreThanOneFraudulent.txt"));

            result.Should().NotBeNull("The result should not be null.");
            result.Count.Should().Be(2, "The result should contains the number of lines of the file");
        }

        private static List<FraudResult> ExecuteTest(string filePath)
        {
            using (var reader = File.OpenText(filePath))
            {
                var logger = new StructuredLogger();
                var fraudRadar = new FraudRadar(
                    logger,
                    new NormalizedOrdersProvider(
                        new OrdersProvider(logger),
                        new OrderNormalizer(new IOrderVisitor[]
                        {
                            new EmailNormalizer(),
                            new StateNormalizer(new Dictionary<string, string>
                            {
                                { "il", "illinois" },
                                { "ca", "california" },
                                { "ny", "new york" }
                            }),
                            new StreetNormalizer(new Dictionary<string, string>
                            {
                                { "st.", "street" },
                                { "rd.", "road" }
                            }),
                            new Trimmer(order => order.City, (order, value) => order.City = value),
                            new Trimmer(order => order.ZipCode, (order, value) => order.ZipCode = value),
                            new Trimmer(order => order.CreditCard, (order, value) => order.CreditCard = value)
                        })),
                    new FraudsDetector(new IFraudStrategy[]
                    {
                        new AddressFraudStrategy(),
                        new EmailFraudStrategy()
                    }));


                return fraudRadar.Check(reader).ToList();
            }
        }
    }
}
