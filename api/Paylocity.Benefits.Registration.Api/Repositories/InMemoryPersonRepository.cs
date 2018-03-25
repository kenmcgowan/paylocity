using Paylocity.Benefits.Registration.Api.Exceptions;
using Paylocity.Benefits.Registration.Api.Interfaces;
using Paylocity.Benefits.Registration.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Paylocity.Benefits.Registration.Api.Repositories
{
    // A few notes regarding this class: In the interest of keeping the scope of the code sample scope
    // reasonable, I chose to use a simple in-memory store where data is stored in dictionaries. For the
    // same reasons, I also chose not to implement many of the defensive techniques that you would
    // normally find here (parameter checking, etc.). Those techniques are used throughout the
    // rest of the sample, so I didn't feel repeating it here would demonstrate anything new or distinct.

    /// <summary>
    /// Provides a simple in-memory store for the demo application.
    /// </summary>
    public class InMemoryPersonRepository : IPersonRepository
    {
        private readonly IDictionary<long, Employee> _employeeMap;
        private readonly IDictionary<long, Dependent> _dependentMap;

        /// <summary>
        /// Initializes a new instance of the in-memory person repository.
        /// </summary>
        public InMemoryPersonRepository()
        {
            _employeeMap = new Dictionary<long, Employee>();
            _dependentMap = new Dictionary<long, Dependent>();
        }

        /// <summary>
        /// Initializes a new instance of the in-memory person repository using dictionaries
        /// provided by the caller (primarily to facilitate testing).
        /// </summary>
        /// <param name="employeeMap"></param>
        /// <param name="dependentMap"></param>
        public InMemoryPersonRepository(
            IDictionary<long, Employee> employeeMap,
            IDictionary<long, Dependent> dependentMap)
        {
            _employeeMap = employeeMap;
            _dependentMap = dependentMap;
        }

        public Employee GetEmployee(long employeeId)
        {
            if (!_employeeMap.ContainsKey(employeeId))
            {
                throw new NonexistentDataException(employeeId.ToString());
            }

            return _employeeMap[employeeId];
        }

        public Employee StoreEmployee(EmployeeInfo employeeInfo)
        {
            var id = InMemoryPersonRepository.GetNewId();

            var newEmployee = new Employee
            {
                Id = id,
                FirstName = employeeInfo.FirstName,
                LastName = employeeInfo.LastName,
                AnnualSalary = employeeInfo.AnnualSalary,
                AnnualBenefitsCost = employeeInfo.AnnualBenefitsCost,
                Notes = employeeInfo.Notes
            };

            _employeeMap[id] = newEmployee;

            return newEmployee;
        }

        public Dependent StoreDependent(DependentInfo dependentInfo)
        {
            var id = InMemoryPersonRepository.GetNewId();

            var newDependent = new Dependent
            {
                Id = id,
                EmployeeId = dependentInfo.EmployeeId,
                FirstName = dependentInfo.FirstName,
                LastName = dependentInfo.LastName,
                AnnualBenefitsCost = dependentInfo.AnnualBenefitsCost,
                Notes = dependentInfo.Notes
            };

            _dependentMap[id] = newDependent;

            return newDependent;
        }

        public IEnumerable<Dependent> GetEmployeeDependents(long employeeId)
        {
            return _dependentMap.Values.Where(dependent => dependent.EmployeeId == employeeId);
        }

        /// Provides a way to generate ID's for the in-memory store.
        private static long NextId = 1L;
        private static long GetNewId()
        {
            
            return Interlocked.Increment(ref InMemoryPersonRepository.NextId);
        }
    }
}
