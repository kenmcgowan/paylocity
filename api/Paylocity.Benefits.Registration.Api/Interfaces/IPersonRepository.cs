using Paylocity.Benefits.Registration.Api.Models;
using System.Collections.Generic;

namespace Paylocity.Benefits.Registration.Api.Interfaces
{
    public interface IPersonRepository
    {
        Employee StoreEmployee(string firstName, string lastName, decimal annualSalary, decimal annualBenefitsCost, string notes);
        Dependent StoreDependent(long employeeId, string firstName, string lastName, decimal annualBenefitsCost, string notes);
        Employee GetEmployee(long employeeId);
        IEnumerable<Dependent> GetEmployeeDependents(long employeeId);
    }
}
