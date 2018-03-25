using Paylocity.Benefits.Registration.Api.Exceptions;
using Paylocity.Benefits.Registration.Api.Interfaces;
using Paylocity.Benefits.Registration.Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace Paylocity.Benefits.Registration.Api.Repositories
{
    public class InMemoryPersonRepository : IPersonRepository
    {
        private readonly IDictionary<long, Employee> _employeeMap;
        private readonly IDictionary<long, Dependent> _dependentMap;

        public InMemoryPersonRepository()
        {
            _employeeMap = new Dictionary<long, Employee>();
            _dependentMap = new Dictionary<long, Dependent>();
        }

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

        public Employee StoreEmployee(string firstName, string lastName, decimal annualSalary, decimal annualBenefitsCost, string notes)
        {
            var id = InMemoryPersonRepository.GetNewId();

            var newEmployee = new Employee
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                AnnualSalary = annualSalary,
                AnnualBenefitsCost = annualBenefitsCost,
                Notes = notes
            };

            _employeeMap[id] = newEmployee;

            return newEmployee;
        }

        public Dependent StoreDependent(long employeeId, string firstName, string lastName, decimal annualBenefitsCost, string notes)
        {
            var id = InMemoryPersonRepository.GetNewId();

            var newDependent = new Dependent
            {
                Id = id,
                EmployeeId = employeeId,
                FirstName = firstName,
                LastName = lastName,
                AnnualBenefitsCost = annualBenefitsCost,
                Notes = notes
            };

            _dependentMap[id] = newDependent;

            return newDependent;
        }

        public IEnumerable<Dependent> GetEmployeeDependents(long employeeId)
        {
            return _dependentMap.Values.Where(dependent => dependent.EmployeeId == employeeId);
        }

        private static long NextId = 1L;
        private static long GetNewId()
        {
            return InMemoryPersonRepository.NextId++;
        }
    }
}
