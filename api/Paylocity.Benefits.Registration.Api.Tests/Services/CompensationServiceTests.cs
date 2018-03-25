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

            Assert.Throws<ArgumentNullException>(() => sut.GetAnnualSalary(null));
        }

        [Fact]
        public void GetEmployeeCompensation_ValidPerson_ReturnsCorrectAnnualCompensation()
        {
            var validPerson = new Person { FirstName = "Some", LastName = "Employee" };
            var expectedSalary = 52000.00M;

            var sut = new CompensationService();
            var actualSalary = sut.GetAnnualSalary(validPerson);

            Assert.Equal(expectedSalary, actualSalary);
        }
    }
}
