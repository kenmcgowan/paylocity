using System.Collections.Generic;

namespace Paylocity.Benefits.Registration.Api.Interfaces
{
    public interface IPaymentCalculator
    {
        IList<decimal> CalculatePayments(decimal total, int paymentCount);
    }
}
