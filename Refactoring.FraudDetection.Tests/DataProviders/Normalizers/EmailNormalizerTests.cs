using System;
using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DataProviders.Normalizers;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomainLogic.Entities;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.Tests.DataProviders.Normalizers
{
    [TestClass]
    public class EmailNormalizerTests
    {
        private Fixture _fixture;
        private EmailNormalizer _normalizer;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _normalizer = new EmailNormalizer();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Visit_NullOrder_ArgumentNullExceptionExpected()
        {
            _normalizer.Visit(null);
        }

        [TestMethod]
        public void Visit_NullEmail_SameOrder()
        {
            var order = _fixture.Build<Order>()
                .Without(x => x.Email)
                .Create();

            var actual = _normalizer.Visit(order);

            actual.Should().BeEquivalentTo(order);
        }

        [TestMethod]
        [ExpectedException(typeof(NormalizationException))]
        public void Visit_EmailWithoutDomaim_NormalizationExceptions()
        {
            var order = _fixture.Build<Order>()
                .With(x => x.Email, " sample@ ")
                .Create();

            _normalizer.Visit(order);
        }

        [TestMethod]
        public void Visit_EmailWithPlus_RemovedAfterPlus()
        {
            var order = _fixture.Build<Order>()
                .With(x => x.Email, " sample+tag@domail.com ")
                .Create();
            var expected = new Order(order)
            {
                Email = "sample@domail.com"
            };

            var actual = _normalizer.Visit(order);

            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void Visit_EmailWithDotAndPlus_RemovedAfterPlus()
        {
            var order = _fixture.Build<Order>()
                .With(x => x.Email, "sa.mp.le+tag@domail.com")
                .Create();
            var expected = new Order(order)
            {
                Email = "sample@domail.com"
            };

            var actual = _normalizer.Visit(order);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
