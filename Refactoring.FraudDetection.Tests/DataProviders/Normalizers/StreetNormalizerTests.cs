using System;
using System.Collections.Generic;
using AutoFixture;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DataProviders.Normalizers;
using Payvision.CodeChallenge.Refactoring.FraudDetection.DomanLogic.Entities;

namespace Payvision.CodeChallenge.Refactoring.FraudDetection.Tests.DataProviders.Normalizers
{
    [TestClass]
    public class StreetNormalizerTests
    {
        private Fixture _fixture;
        private StreetNormalizer _normalizer;
        private Dictionary<string, string> _replacements;

        [TestInitialize]
        public void Initialize()
        {
            _fixture = new Fixture();
            _replacements = new Dictionary<string, string>
            {
                { "rd.", "road" }
            };

            _normalizer = new StreetNormalizer(_replacements);
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
                .Without(x => x.Street)
                .Create();

            var actual = _normalizer.Visit(order);

            actual.Should().BeEquivalentTo(order);
        }

        [TestMethod]
        public void Visit_UnknownReplasement_StreetShouldStay()
        {
            var order = _fixture.Build<Order>()
                .With(x => x.Street, "rd. illinois")
                .Create();

            var actual = _normalizer.Visit(order);

            actual.Should().BeEquivalentTo(order);
        }

        [TestMethod]
        public void Visit_KnownReplasement_ShouldStay()
        {
            var order = _fixture.Build<Order>()
                .With(x => x.Street, "rd.")
                .Create();
            var expected = new Order(order)
            {
                Street = _replacements[order.Street]
            };

            var actual = _normalizer.Visit(order);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
