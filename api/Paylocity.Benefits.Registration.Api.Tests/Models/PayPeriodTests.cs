using Paylocity.Benefits.Registration.Api.Models;
using Xunit;

namespace Paylocity.Benefits.Registration.Api.Tests.Models
{
    public class PayPeriodTests
    {
        public void Equals_OtherFieldsAreAllEqual_ReturnsTrue()
        {
            var sameNumber = 1;
            var sameGrossPay = 1234.00M;
            var sameDeductions = 100.00M;
            var sameNetPay = 15.00M;

            var payPeriod1 = new PayPeriod
            {
                Number = sameNumber,
                GrossPay = sameGrossPay,
                Deductions = sameDeductions,
                NetPay = sameNetPay
            };

            var payPeriod2 = new PayPeriod
            {
                Number = sameNumber,
                GrossPay = sameGrossPay,
                Deductions = sameDeductions,
                NetPay = sameNetPay
            };

            Assert.True(payPeriod1.Equals(payPeriod2));
            Assert.True(payPeriod2.Equals(payPeriod1));
            Assert.Equal(payPeriod1, payPeriod2);
        }

        public void Equals_OtherFieldsAreNotAllEqual_ReturnsFalse()
        {
            var payPeriod1Number = 1;
            var payPeriod2Number = 2;
            var sameGrossPay = 1234.00M;
            var sameDeductions = 100.00M;
            var sameNetPay = 15.00M;

            var payPeriod1 = new PayPeriod
            {
                Number = payPeriod1Number,
                GrossPay = sameGrossPay,
                Deductions = sameDeductions,
                NetPay = sameNetPay
            };

            var payPeriod2 = new PayPeriod
            {
                Number = payPeriod2Number,
                GrossPay = sameGrossPay,
                Deductions = sameDeductions,
                NetPay = sameNetPay
            };

            Assert.False(payPeriod1.Equals(payPeriod2));
            Assert.False(payPeriod2.Equals(payPeriod1));
            Assert.NotEqual(payPeriod1, payPeriod2);
        }
    }
}
