using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Paylocity.Benefits.Registration.Api.Services.Tests
{
    public class EvenDistributionPaymentCalculatorTests
    {
        [Theory]
        [MemberData(nameof(InvalidPaymentCounts))]
        public void CalculatePayments_NegativePaymentCount_ThrowsArgumentOutOfRangeException(int invalidPaymentCount)
        {
            var validTotal = 1000.0M;
            var sut = new EvenDistributionPaymentCalculator();

            sut.Invoking(calculator => calculator.CalculatePayments(validTotal, invalidPaymentCount))
                .Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory]
        [MemberData(nameof(NonzeroPaymentCountsAndTotals))]
        public void CalculatePayments_ValidPaymentCountAndTotals_ReturnsCorrectNumberOfPaymentsWithCorrectTotal(decimal expectedTotal, int expectedPaymentCount)
        {
            var sut = new EvenDistributionPaymentCalculator();

            var actualPayments = sut.CalculatePayments(expectedTotal, expectedPaymentCount);

            actualPayments.Should().NotBeNull();

            using (new FluentAssertions.Execution.AssertionScope())
            {
                actualPayments.Count().Should().Be(expectedPaymentCount);
                actualPayments.Sum().Should().Be(expectedTotal);
            }
        }

        [Theory]
        [MemberData(nameof(NonzeroPaymentCountsAndTotals))]
        public void CalculatePayments_ValidPaymentCountAndTotals_PaymentsDoNotDifferByMoreThanOneCent(decimal validTotal, int validPaymentCount)
        {
            var sut = new EvenDistributionPaymentCalculator();

            var payments = sut.CalculatePayments(validTotal, validPaymentCount);

            payments.Should().NotBeNull();
            payments.Count().Should().BeGreaterOrEqualTo(1);

            var firstPayment = payments[0];

            payments.All(payment => Math.Abs(payment - firstPayment) <= 0.01M)
                .Should().BeTrue("the distribution is evened out so no payment differs from the first payment by more than one penny");
        }

        #region test data
        private static int[] _invalidPaymentCounts = new[] { -99, -1, 0 };
        private static decimal[] _validTotals = new[] { -1000000000.0M, -99.99M, -1.0M, -0.5M, 0.0M, 0.5M, 1.0M, 99.99M, 1000.0M, 1000000000.0M };
        private static int[] _validPaymentCounts = new[] { 1, 2, 4, 26, 99, 1000 };
        private static decimal[] _totalsWithFractionalCents = new[] { -1000000000.0003M, -99.9999999M, -1.00001M, -0.00002M, 0.000005M, 0.009M, 1.000001M, 99.9999999M, 1000000000.000005M };

        public static IEnumerable<object[]> InvalidPaymentCounts => EvenDistributionPaymentCalculatorTests._invalidPaymentCounts.Select(paymentCount => new object[] { paymentCount });

        public static IEnumerable<object[]> NonzeroPaymentCountsAndTotals
        {
            get
            {
                // Return all combinations of the valid totals and valid payment counts
                return EvenDistributionPaymentCalculatorTests._validTotals.SelectMany(total => EvenDistributionPaymentCalculatorTests._validPaymentCounts,
                    (total, paymentCount) => new object[] { total, paymentCount });
            }
        }
        #endregion
    }
}
