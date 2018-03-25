using Paylocity.Benefits.Registration.Api.Models;
using System.Collections.Generic;

namespace Paylocity.Benefits.Registration.Api.Interfaces
{
    public interface IPersonRepository
    {
        Employee StoreEmployee(EmployeeInfo employeeInfo);
        Dependent StoreDependent(DependentInfo dependentInfo);
        Employee GetEmployee(long employeeId);
        IEnumerable<Dependent> GetEmployeeDependents(long employeeId);
    }
}
