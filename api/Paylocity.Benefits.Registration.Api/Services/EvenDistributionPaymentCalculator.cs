using Paylocity.Benefits.Registration.Api.Interfaces;
using System;
using System.Collections.Generic;

namespace Paylocity.Benefits.Registration.Api.Services
{
    /// <summary>
    /// Implements the <see cref="Paylocity.Deductions.Api.Interfaces.IPaymentCalculator"/> interface;
    /// attempts to distribute payments as evenly as possible.
    /// </summary>
    /// <remarks>
    /// Implements a means of dividing a total amount of money into a specific number of payments,
    /// rounding payments to whole cents and distributing the total evenly across all payments
    /// while avoiding accumulative rounding errors.
    /// </remarks>
    public class EvenDistributionPaymentCalculator : IPaymentCalculator
    {
        public IList<decimal> CalculatePayments(decimal totalAmount, int paymentCount)
        {
            if (paymentCount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(paymentCount));
            }

            var result = new decimal[paymentCount];
            var totalPaidUpToPriorIteration = 0.0M;
            var totalNumberOfPayments = (decimal)paymentCount;

            for (int index = 0; index < paymentCount; ++index)
            {
                var paymentNumber = (decimal)(index + 1);

                var totalPaidIncludingThisIteration = Math.Round(totalAmount * paymentNumber / totalNumberOfPayments, 2);

                // The next payment is simply the amount that should have been paid up through this iteration minus
                // the amount paid out so far. Calculating it this way balances out all the fractional cents that have
                // been being eliminated/accumulated by rounding in earlier payments.
                result[index] = totalPaidIncludingThisIteration - totalPaidUpToPriorIteration;

                totalPaidUpToPriorIteration = totalPaidIncludingThisIteration;
            }

            return result;
        }
    }
}
