using Paylocity.Benefits.Registration.Api.Models;
using Xunit;

namespace Paylocity.Benefits.Registration.Api.Tests.Models
{
    public class DependentTests
    {
        public void Equals_OtherFieldsAreAllEqual_ReturnsTrue()
        {
            var sameId = 2L;
            var sameEmployeeId = 1L;
            var sameFirstName = "Brigitte";
            var sameLastName = "Hirsch";
            var sameAnnualBenefitsCost = 1.00M;
            var sameNotes = "Okay then.";

            var dependent1 = new Dependent
            {
                Id = sameId,
                EmployeeId = sameEmployeeId,
                FirstName = sameFirstName,
                LastName = sameLastName,
                AnnualBenefitsCost = sameAnnualBenefitsCost,
                Notes = sameNotes
            };

            var dependent2 = new Dependent
            {
                Id = sameId,
                EmployeeId = sameEmployeeId,
                FirstName = sameFirstName,
                LastName = sameLastName,
                AnnualBenefitsCost = sameAnnualBenefitsCost,
                Notes = sameNotes
            };

            Assert.True(dependent1.Equals(dependent2));
            Assert.True(dependent2.Equals(dependent1));
            Assert.Equal(dependent1, dependent2);
        }

        public void Equals_OtherFieldsAreNotAllEqual_ReturnsFalse()
        {
            var idForDependent1 = 2L;
            var idForDependent2 = 3L;
            var sameEmployeeId = 1L;
            var sameFirstName = "Lorenzo";
            var sameLastName = "Loannidis";
            var sameAnnualBenefitsCost = 2400;
            var sameNotes = "Some other note";

            var dependent1 = new Dependent
            {
                Id = idForDependent1,
                EmployeeId = sameEmployeeId,
                FirstName = sameFirstName,
                LastName = sameLastName,
                AnnualBenefitsCost = sameAnnualBenefitsCost,
                Notes = sameNotes
            };

            var dependent2 = new Dependent
            {
                Id = idForDependent2,
                EmployeeId = sameEmployeeId,
                FirstName = sameFirstName,
                LastName = sameLastName,
                AnnualBenefitsCost = sameAnnualBenefitsCost,
                Notes = sameNotes
            };

            Assert.False(dependent1.Equals(dependent2));
            Assert.False(dependent2.Equals(dependent1));
            Assert.NotEqual(dependent1, dependent2);
        }
    }
}
