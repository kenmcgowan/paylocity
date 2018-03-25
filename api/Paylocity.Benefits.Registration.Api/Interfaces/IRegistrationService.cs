using Paylocity.Benefits.Registration.Api.Models;
using System.Collections.Generic;

namespace Paylocity.Benefits.Registration.Api.Interfaces
{
    public interface IRegistrationService
    {
        Employee RegisterEmployee(Person person);
        Dependent RegisterDependent(long employeeId, Person person);
        IEnumerable<PayPeriod> PreviewPayPeriods(long employeeId);
    }
}
