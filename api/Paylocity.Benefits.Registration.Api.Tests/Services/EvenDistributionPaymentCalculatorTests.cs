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

            Assert.Throws<ArgumentOutOfRangeException>(() => sut.CalculatePayments(validTotal, invalidPaymentCount));
        }

        [Theory]
        [MemberData(nameof(NonzeroPaymentCountsAndTotals))]
        public void CalculatePayments_ValidPaymentCountAndTotals_ReturnsCorrectNumberOfPayments(decimal validTotal, int expectedPaymentCount)
        {
            var sut = new EvenDistributionPaymentCalculator();

            var payments = sut.CalculatePayments(validTotal, expectedPaymentCount);
            var actualPaymentCount = payments.Count();

            Assert.Equal(expectedPaymentCount, actualPaymentCount);
        }

        [Theory]
        [MemberData(nameof(NonzeroPaymentCountsAndTotals))]
        public void CalculatePayments_ValidPaymentCountAndTotals_SumOfAllPaymentsEqualsTotal(decimal expectedTotal, int validPaymentCount)
        {
            var sut = new EvenDistributionPaymentCalculator();

            var payments = sut.CalculatePayments(expectedTotal, validPaymentCount);
            var actualTotal = payments.Sum();

            Assert.Equal(expectedTotal, actualTotal);
        }

        [Theory]
        [MemberData(nameof(NonzeroPaymentCountsAndTotals))]
        public void CalculatePayments_ValidPaymentCountAndTotals_PaymentsDoNotDifferByMoreThanOneCent(decimal validTotal, int validPaymentCount)
        {
            var sut = new EvenDistributionPaymentCalculator();

            var payments = sut.CalculatePayments(validTotal, validPaymentCount);
            var firstPayment = payments[0];

            for (int index = 1; index < payments.Count(); ++index)
            {
                var difference = Math.Abs(payments[index] - firstPayment);
                Assert.True(difference <= 0.01M, $"The first payment and payment {index} differed by more than 1 cent (difference = {difference}");
            }
        }

        [Theory]
        [MemberData(nameof(ValidPaymentCountsAndTotalsWithFractionalCents))]
        public void CalculatePayments_TotalsWithFractionalCents_SumOfAllPaymentsEqualsTotalWithoutFractionalCents(decimal totalWithFractionalCents, int validPaymentCount)
        {
            var sut = new EvenDistributionPaymentCalculator();

            var payments = sut.CalculatePayments(totalWithFractionalCents, validPaymentCount);
            var actualTotal = payments.Sum();
            var difference = Math.Abs(totalWithFractionalCents - actualTotal);

            Assert.NotEqual(0.0M, difference);
            Assert.True(difference < 0.01M, "The difference between the sum of payments and the total >= 0.01");
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

        public static IEnumerable<object[]> ValidPaymentCountsAndTotalsWithFractionalCents
        {
            get
            {
                // Return all combinations of the totals with fractional cents and valid payment counts
                return EvenDistributionPaymentCalculatorTests._totalsWithFractionalCents.SelectMany(total => EvenDistributionPaymentCalculatorTests._validPaymentCounts,
                    (total, paymentCount) => new object[] { total, paymentCount });
            }
        }
        #endregion
    }
}
