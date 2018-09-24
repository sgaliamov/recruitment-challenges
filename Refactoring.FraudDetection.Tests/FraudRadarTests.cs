using System;
using System.IO;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomainLogic;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomainLogic.Entities;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomainLogic.FraudDetectors;
using Payvision.CodeChallenge.Refactoring.FraudDetection.Shared;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.Tests
{
    [TestClass]
    public class FraudRadarTests
    {
        private Mock<IFraudsDetector> _detector;
        private FraudRadar _fraudRadar;
        private Mock<IStructuredLogger> _logger;
        private Mock<IOrdersProvider> _ordersProvider;
        private Fixture _fixture;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _logger = new Mock<IStructuredLogger>();
            _ordersProvider = new Mock<IOrdersProvider>(MockBehavior.Strict);
            _detector = new Mock<IFraudsDetector>(MockBehavior.Strict);

            _fraudRadar = new FraudRadar(_logger.Object, _ordersProvider.Object, _detector.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _ordersProvider.VerifyAll();
            _detector.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Check_NullReader_ArgumentNullExceptionExpected()
        {
            _fraudRadar.Check(null);
        }

        [TestMethod]
        public void Check_NormalFlow()
        {
            var orders = _fixture.CreateMany<Order>().ToArray();
            var expected = _fixture.CreateMany<FraudResult>().ToArray();

            using (var reader = new StreamReader(new MemoryStream()))
            {
                // ReSharper disable once AccessToDisposedClosure
                _ordersProvider.Setup(x => x.ReadOrders(reader)).Returns(orders);
                _detector.Setup(x => x.CheckOrders(orders)).Returns(expected);

                // act
                var results = _fraudRadar.Check(reader).ToArray();

                // assert
                results.Should().BeEquivalentTo(expected.AsEnumerable());
            }
        }
    }
}
