﻿using FluentAssertions;
using Newtonsoft.Json.Linq;
using Paylocity.Benefits.Registration.Api;
using Paylocity.Benefits.Registration.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Paylocity.Benefits.Registration.IntegrationTests
{
    [Collection("Deductions API Integration Tests")]
    public class RegistrationApiIntegrationTests
    {
        private readonly TestContext _testContext;

        public RegistrationApiIntegrationTests(TestContext testContext)
        {
            _testContext = testContext;
        }

        [Fact]
        public async Task RegisterEmployee_InvalidPerson_Returns400()
        {
            var invalidPersonJson = RegistrationApiIntegrationTests.GetPersonJson(firstName: string.Empty, lastName: string.Empty);
            var invalidHttpContent = new StringContent(invalidPersonJson, Encoding.UTF8, "text/json");

            var response = await _testContext.Client.PostAsync("/benefits/registration/employees", invalidHttpContent);

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task RegisterEmployee_ValidPerson_Returns201AndStoresEmployee()
        {
            var expectedFirstName = "Alejandra";
            var expectedLastName = "Schwartz";
            var validPersonJson = RegistrationApiIntegrationTests.GetPersonJson(expectedFirstName, expectedLastName);
            var validHttpContent = new StringContent(validPersonJson, Encoding.UTF8, "text/json");

            using (TestDataStores.Scope())
            {
                var response = await _testContext.Client.PostAsync("/benefits/registration/employees", validHttpContent);
                var storedEmployee = TestDataStores.EmployeeStore.Values.Where(
                    employee => (employee.FirstName == expectedFirstName) && (employee.LastName == expectedLastName))
                    .SingleOrDefault();

                response.Should().NotBeNull();

                using (new FluentAssertions.Execution.AssertionScope())
                {
                    response.StatusCode.Should().Be(HttpStatusCode.Created);
                    storedEmployee.Should().NotBeNull();
                }
            }
        }

        [Fact]
        public async Task RegisterDependent_InvalidPerson_Returns400()
        {
            var irrelevantEmployeeId = 0L;
            var invalidPersonJson = RegistrationApiIntegrationTests.GetPersonJson(firstName: string.Empty, lastName: string.Empty);
            var invalidHttpContent = new StringContent(invalidPersonJson, Encoding.UTF8, "text/json");

            var response = await _testContext.Client.PostAsync($"/benefits/registration/employees/{irrelevantEmployeeId}/dependents", invalidHttpContent);

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task RegisterDependent_ValidPerson_Returns201AndStoresDependent()
        {
            var expectedEmployeeId = 1L;
            var expectedFirstName = "Shinobu";
            var expectedLastName = "Huang";
            var validDependentJson = RegistrationApiIntegrationTests.GetPersonJson(expectedFirstName, expectedLastName);
            var validHttpContent = new StringContent(validDependentJson, Encoding.UTF8, "text/json");

            using (TestDataStores.Scope())
            {
                var response = await _testContext.Client.PostAsync($"/benefits/registration/employees/{expectedEmployeeId}/dependents", validHttpContent);
                var storedDependent = TestDataStores.DependentStore.Values.Where(
                    dependent => (dependent.FirstName == expectedFirstName) && (dependent.LastName == expectedLastName))
                    .SingleOrDefault();

                response.Should().NotBeNull();

                using (new FluentAssertions.Execution.AssertionScope())
                {
                    response.StatusCode.Should().Be(HttpStatusCode.Created);
                    storedDependent.Should().NotBeNull();
                }
            }
        }

        [Fact]
        public async Task PreviewPayPeriods_NonexistentEmployee_Returns404()
        {
            var idForNonexistentEmployee = long.MinValue;
            var response = await _testContext.Client.GetAsync($"/benefits/registration/employees/{idForNonexistentEmployee}/payperiods");
            var actualContent = await response.Content.ReadAsStringAsync();

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task PreviewPayPeriods_EmployeeWithDependents_Returns200AndCorrectLineItems()
        {
            var employee = new Employee
            {
                Id = 1L,
                AnnualSalary = DemoConstants.NUMBER_OF_PAY_PERIODS_PER_YEAR * 3.00M,
                AnnualBenefitsCost = DemoConstants.NUMBER_OF_PAY_PERIODS_PER_YEAR * 1.00M
            };
            var dependent = new Dependent
            {
                Id = 2L,
                EmployeeId = employee.Id,
                AnnualBenefitsCost = DemoConstants.NUMBER_OF_PAY_PERIODS_PER_YEAR * 1.00M
            };
            var expectedPayPeriods = Enumerable.Range(1, DemoConstants.NUMBER_OF_PAY_PERIODS_PER_YEAR)
                .Select(number => new PayPeriod
                {
                    Number = number,
                    GrossPay = 3.00M,
                    Deductions = 2.00M,
                    NetPay = 1.00M
                });
            var expectedContent = JObject.Parse(RegistrationApiIntegrationTests.GetPayPeriodsJson(expectedPayPeriods));

            using (TestDataStores.Scope())
            {
                TestDataStores.EmployeeStore[employee.Id] = employee;
                TestDataStores.DependentStore[dependent.Id] = dependent;

                var response = await _testContext.Client.GetAsync($"/benefits/registration/employees/{employee.Id}/payperiods");
                var responseBody = await response.Content.ReadAsStringAsync();
                var actualContent = JObject.Parse(responseBody);

                response.Should().NotBeNull();

                using (new FluentAssertions.Execution.AssertionScope())
                {
                    response.StatusCode.Should().Be(HttpStatusCode.OK);
                    JToken.DeepEquals(expectedContent, actualContent)
                        .Should().BeTrue("ignoring whitespace, the JSON results should be equivalent");
                }
            }
        }

        private static string GetPersonJson(string firstName, string lastName)
        {
            return $"{{ \"firstName\": \"{firstName}\", \"lastName\": \"{lastName}\" }}";
        }

        private static string GetEmployeeJson(Employee employee)
        {
            return $"{{ \"id\": {employee.Id}, \"firstName\": \"{employee.FirstName}\", " +
                $"\"lastName\": \"{employee.LastName}\", \"annualSalary\": \"{employee.AnnualSalary}\", " +
                $"\"annualBenefitsCost\": \"{employee.AnnualBenefitsCost}\", \"notes\": \"{employee.Notes}\" }}";
        }

        private static string GetDependentJson(Dependent dependent)
        {
            return $"{{ \"id\": {dependent.Id}, \"employeeId\": {dependent.EmployeeId}, " +
                $"\"firstName\": \"{dependent.FirstName}\", \"lastName\": \"{dependent.LastName}\", " +
                $"\"annualBenefitsCost\": \"{dependent.AnnualBenefitsCost}\", \"notes\": \"{dependent.Notes}\" }}";
        }

        private static string GetPayPeriodsJson(IEnumerable<PayPeriod> payPeriods)
        {
            var buffer = new StringBuilder("{\"payPeriods\": [ ");
            payPeriods.ToList().ForEach(payPeriod => buffer.Append($"{{\"number\": {payPeriod.Number}, " +
                $"\"grossPay\": \"{payPeriod.GrossPay.ToString("F2")}\", " +
                $"\"deductions\": \"{payPeriod.Deductions.ToString("F2")}\", " +
                $"\"netPay\": \"{payPeriod.NetPay.ToString("F2")}\"}},"));
            buffer.Append(" ]}");
            return buffer.ToString();
        }
    }
}
