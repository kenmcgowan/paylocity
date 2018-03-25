using Paylocity.Benefits.Registration.Api.Interfaces;
using Paylocity.Benefits.Registration.Api.Models;
using System;

namespace Paylocity.Benefits.Registration.Api.Services
{
    public class CompensationService : ICompensationService
    {
        public decimal GetAnnualSalary(Person employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            if (string.IsNullOrEmpty(employee.FirstName?.Trim()) ||
                string.IsNullOrEmpty(employee.LastName?.Trim()))
            {
                throw new ArgumentException(nameof(employee));
            }

            // This is where the "contrived' part of this sample comes in. :)
            return DemoConstants.NUMBER_OF_PAY_PERIODS_PER_YEAR * DemoConstants.EMPLOYEE_SALARY_PER_PAY_PERIOD;
        }
    }
}
