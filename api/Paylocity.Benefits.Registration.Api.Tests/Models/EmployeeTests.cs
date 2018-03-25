using Paylocity.Benefits.Registration.Api.Models;
using Xunit;

namespace Paylocity.Benefits.Registration.Api.Tests.Models
{
    public class EmployeeTests
    {
        public void Equals_OtherFieldsAreAllEqual_ReturnsTrue()
        {
            var sameId = 1L;
            var sameFirstName = "Ferdinand";
            var sameLastName = "Morandi";
            var sameAnnualSalary = 112358.00M;
            var sameAnnualBenefitsCost = 2468.00M;
            var sameNotes = "Somethingerother";

            var employee1 = new Employee
            {
                Id = sameId,
                FirstName = sameFirstName,
                LastName = sameLastName,
                AnnualSalary = sameAnnualSalary,
                AnnualBenefitsCost = sameAnnualBenefitsCost,
                Notes = sameNotes
            };

            var employee2 = new Employee
            {
                Id = sameId,
                FirstName = sameFirstName,
                LastName = sameLastName,
                AnnualSalary = sameAnnualSalary,
                AnnualBenefitsCost = sameAnnualBenefitsCost,
                Notes = sameNotes
            };

            Assert.True(employee1.Equals(employee2));
            Assert.True(employee2.Equals(employee1));
            Assert.Equal(employee1, employee2);
        }

        public void Equals_OtherFieldsAreNotAllEqual_ReturnsFalse()
        {
            var idForEmployee1 = 1L;
            var idForEmployee2 = 2L;
            var sameFirstName = "Nawra";
            var sameLastName = "Senft";
            var sameAnnualSalary = 112358.00M;
            var sameAnnualBenefitsCost = 2468.00M;
            var sameNotes = "Some note";

            var employee1 = new Employee
            {
                Id = idForEmployee1,
                FirstName = sameFirstName,
                LastName = sameLastName,
                AnnualSalary = sameAnnualSalary,
                AnnualBenefitsCost = sameAnnualBenefitsCost,
                Notes = sameNotes
            };

            var employee2 = new Employee
            {
                Id = idForEmployee2,
                FirstName = sameFirstName,
                LastName = sameLastName,
                AnnualSalary = sameAnnualSalary,
                AnnualBenefitsCost = sameAnnualBenefitsCost,
                Notes = sameNotes
            };

            Assert.False(employee1.Equals(employee2));
            Assert.False(employee2.Equals(employee1));
            Assert.NotEqual(employee1, employee2);
        }
    }
}
