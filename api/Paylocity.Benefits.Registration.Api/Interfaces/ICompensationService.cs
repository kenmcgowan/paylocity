using Paylocity.Benefits.Registration.Api.Models;

namespace Paylocity.Benefits.Registration.Api.Interfaces
{
    public interface ICompensationService
    {
        decimal GetAnnualSalary(Person employee);
    }
}
