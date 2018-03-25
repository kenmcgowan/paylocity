using FluentAssertions;
using Paylocity.Benefits.Registration.Api.Models;
using Paylocity.Benefits.Registration.Api.Services;
using System;
using Xunit;

namespace Paylocity.Benefits.Registration.Api.Tests.Services
{
    public class CompensationServiceTests
    {
        [Fact]
        public void GetEmployeeCompensation_NullEmployee_ThrowsArgumentNullException()
        {
            var sut = new CompensationService();

            sut.Invoking(compensationService => compensationService.GetAnnualSalary(null))
                .Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(new object[] { null, "Jackson" })]
        [InlineData(new object[] { "Derek", "" })]
        public void GetEmployeeCompensation_InvalidEmployee_ThrowsArgumentException(string questionableFirstName, string questionableLastName)
        {
            var invalidPerson = new Person
            {
                FirstName = questionableFirstName,
                LastName = questionableLastName
            };

            var sut = new CompensationService();

            sut.Invoking(compensationService => compensationService.GetAnnualSalary(invalidPerson))
                .Should().Throw<ArgumentException>();
        }

        [Fact]
        public void GetEmployeeCompensation_ValidPerson_ReturnsCorrectAnnualCompensation()
        {
            var validPerson = new Person { FirstName = "Some", LastName = "Employee" };
            var expectedSalary = 52000.00M;

            var sut = new CompensationService();
            var actualSalary = sut.GetAnnualSalary(validPerson);

            actualSalary.Should().Be(expectedSalary);
        }
    }
}
