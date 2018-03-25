using Paylocity.Benefits.Registration.Api.Interfaces;
using Paylocity.Benefits.Registration.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Paylocity.Benefits.Registration.Api.Services
{
    public class RegistrationService : IRegistrationService
    {
        private IPersonRepository _personRepository;
        private ICompensationService _compensationService;
        private IBenefitsService _benefitsService;
        private IPaymentCalculator _paymentCalculator;

        public RegistrationService(
            IPersonRepository personRepository,
            ICompensationService compensationService,
            IBenefitsService benefitsService,
            IPaymentCalculator paymentCalculator)
        {
            _personRepository = personRepository;
            _compensationService = compensationService;
            _benefitsService = benefitsService;
            _paymentCalculator = paymentCalculator;
        }

        public Employee GetEmployee(long employeeId)
        {
            return _personRepository.GetEmployee(employeeId);
        }

        public Employee RegisterEmployee(Person person)
        {
            RegistrationService.CheckPerson(person);

            var annualSalary = _compensationService.GetAnnualSalary(person);
            var benefitsInfo = _benefitsService.GetAnnualEmployeeBenefitsCost(person);

            var employee = _personRepository.StoreEmployee(
                person.FirstName,
                person.LastName,
                annualSalary,
                benefitsInfo.AnnualCost,
                benefitsInfo.Notes);

            return employee;
        }

        public Dependent RegisterDependent(long employeeId, Person person)
        {
            RegistrationService.CheckPerson(person);

            var benefitsInfo = _benefitsService.GetAnnualDependentBenefitsCost(person);

            var dependent = _personRepository.StoreDependent(
                employeeId,
                person.FirstName,
                person.LastName,
                benefitsInfo.AnnualCost,
                benefitsInfo.Notes);

            return dependent;
        }

        public IEnumerable<PayPeriod> PreviewPayPeriods(long employeeId)
        {
            var employee = _personRepository.GetEmployee(employeeId);
            var dependents = _personRepository.GetEmployeeDependents(employee.Id);

            var totalAnnualBenefitsCost = employee.AnnualBenefitsCost + dependents.Sum(dependent => dependent.AnnualBenefitsCost);

            var lineItemNumbers = Enumerable.Range(1, DemoConstants.NUMBER_OF_PAY_PERIODS_PER_YEAR);
            var grossPayLineItems = _paymentCalculator.CalculatePayments(employee.AnnualSalary, DemoConstants.NUMBER_OF_PAY_PERIODS_PER_YEAR);
            var deductionsLineItems = _paymentCalculator.CalculatePayments(totalAnnualBenefitsCost, DemoConstants.NUMBER_OF_PAY_PERIODS_PER_YEAR);

            return lineItemNumbers
                .Zip(grossPayLineItems, (number, grossPay) => new { Number = number, GrossPay = grossPay })
                .Zip(deductionsLineItems, (partialLineItem, deductions) => new PayPeriod
                {
                    Number = partialLineItem.Number,
                    GrossPay = partialLineItem.GrossPay,
                    Deductions = deductions,
                    NetPay = partialLineItem.GrossPay - deductions
                });

            throw new NotImplementedException();
        }

        private static void CheckPerson(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            if (string.IsNullOrEmpty(person.FirstName?.Trim()) ||
                string.IsNullOrEmpty(person.LastName?.Trim()))
            {
                throw new ArgumentException("Invalid name", nameof(person));
            }
        }
    }
}
