using Paylocity.Benefits.Registration.Api.Interfaces;
using Paylocity.Benefits.Registration.Api.Models;
using System;
using System.Globalization;

namespace Paylocity.Benefits.Registration.Api.Services
{
    public class BenefitsService : IBenefitsService
    {
        public BenefitsInfo GetAnnualDependentBenefitsCost(Person dependent)
        {
            return BenefitsService.CalculateAdjustedCostFromBaseCost(dependent, DemoConstants.ANNUAL_DEPENDENT_BENEFITS_COST);
        }

        public BenefitsInfo GetAnnualEmployeeBenefitsCost(Person employee)
        {
            return BenefitsService.CalculateAdjustedCostFromBaseCost(employee, DemoConstants.ANNUAL_EMPLOYEE_BENEFITS_COST);
        }

        private static BenefitsInfo CalculateAdjustedCostFromBaseCost(Person person, decimal baseAnnualBenefitsCost)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            var firstName = person.FirstName?.Trim();
            var lastName = person.LastName?.Trim();

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentException("Invalid name", nameof(person));
            }

            var annualBenefitsCost = baseAnnualBenefitsCost;
            var notes = string.Empty;

            if (firstName.StartsWith("a", ignoreCase: true, culture: CultureInfo.InvariantCulture) ||
                lastName.StartsWith("a", ignoreCase: true, culture: CultureInfo.InvariantCulture))
            {
                annualBenefitsCost *= (1.0M - DemoConstants.MAGIC_ALPHABET_DISCOUNT);
                notes = string.Format("Awesome name, {0:P2} benefits discount!", DemoConstants.MAGIC_ALPHABET_DISCOUNT);
            }

            return new BenefitsInfo { AnnualCost = annualBenefitsCost, Notes = notes };
        }
    }
}
