using Paylocity.Benefits.Registration.Api.Models;
using Paylocity.Benefits.Registration.Api.Services;
using System;
using Xunit;

namespace Paylocity.Benefits.Registration.Api.Tests.Services
{
    public class BenefitsServiceTests
    {
        [Fact]
        public void GetAnnualEmployeeBenefitsCost_NullEmployee_ThrowsArgumentNullException()
        {
            var sut = new BenefitsService();

            Assert.Throws<ArgumentNullException>(() => sut.GetAnnualEmployeeBenefitsCost(null));
        }

        [Theory]
        [InlineData(new object[] {  (string)null, "Smith" })]
        [InlineData(new object[] { "", "Nakamura" })]
        [InlineData(new object[] { "Ravi", "   " })]
        public void GetAnnualEmployeeBenefitsCost_InvalidEmployee_ThrowsArgumentException(string firstName, string lastName)
        {
            var invalidPerson = new Person { FirstName = firstName, LastName = lastName };

            var sut = new BenefitsService();

            Assert.ThrowsAny<ArgumentException>(() => sut.GetAnnualEmployeeBenefitsCost(null));
        }

        [Theory]
        [InlineData(new object[] { "Matvey", "Kawaguchi" })]
        [InlineData(new object[] { " Rhoda", "Gotti" })]
        [InlineData(new object[] { "Mahfuz", "Sorenson  " })]
        public void GetAnnualEmployeeBenefitsCost_ValidEmployeeWithNoDiscount_ReturnsCorrectBenefitsInformation(string firstName, string lastName)
        {
            const decimal expectedAnnualBenefitsCost = 1000.00M;
            var validPersonWithNoDiscount = new Person { FirstName = firstName, LastName = lastName };

            var sut = new BenefitsService();
            var result = sut.GetAnnualEmployeeBenefitsCost(validPersonWithNoDiscount);

            Assert.Equal(expectedAnnualBenefitsCost, result.AnnualCost);
            Assert.Equal(string.Empty, result.Notes);
        }

        [Theory]
        [InlineData(new object[] { "April", "McGowan" })]
        [InlineData(new object[] { "Annibale", "   Deniau" })]
        [InlineData(new object[] { "Haniyya  ", "Ahmed" })]
        public void GetAnnualEmployeeBenefitsCost_ValidEmployeeWithNameDiscount_ReturnsCorrectBenefitsInformation(string firstName, string lastName)
        {
            const decimal expectedAnnualBenefitsCost = 900.00M;
            var validPersonWithDiscount = new Person { FirstName = firstName, LastName = lastName };

            var sut = new BenefitsService();
            var result = sut.GetAnnualEmployeeBenefitsCost(validPersonWithDiscount);

            Assert.Equal(expectedAnnualBenefitsCost, result.AnnualCost);
            Assert.NotNull(result.Notes);
            Assert.True(result.Notes.Length > 0, "The benefits information did not contain any explanatory note");
        }

        [Fact]
        public void GetAnnualDependentBenefitsCost_NullDependent_ThrowsArgumentNullException()
        {
            var sut = new BenefitsService();

            Assert.Throws<ArgumentNullException>(() => sut.GetAnnualDependentBenefitsCost(null));
        }

        [Theory]
        [InlineData(new object[] { (string)null, "Ebrahimi" })]
        [InlineData(new object[] { "", "Devin" })]
        [InlineData(new object[] { "Zakiah", "   " })]
        public void GetAnnualDependentBenefitsCost_InvalidDependent_ThrowsArgumentException(string firstName, string lastName)
        {
            var invalidPerson = new Person { FirstName = firstName, LastName = lastName };

            var sut = new BenefitsService();

            Assert.ThrowsAny<ArgumentException>(() => sut.GetAnnualDependentBenefitsCost(null));
        }

        [Theory]
        [InlineData(new object[] { "Matvey", "Kawaguchi" })]
        [InlineData(new object[] { " Christian ", "Costa" })]
        [InlineData(new object[] { "Sophie", " Gage " })]
        public void GetAnnualDependentBenefitsCost_ValidDependentWithNoDiscount_ReturnsCorrectBenefitsInformation(string firstName, string lastName)
        {
            const decimal expectedAnnualBenefitsCost = 500.00M;
            var validPersonWithNoDiscount = new Person { FirstName = firstName, LastName = lastName };

            var sut = new BenefitsService();
            var result = sut.GetAnnualDependentBenefitsCost(validPersonWithNoDiscount);

            Assert.Equal(expectedAnnualBenefitsCost, result.AnnualCost);
            Assert.Equal(string.Empty, result.Notes);
        }

        [Theory]
        [InlineData(new object[] { "Ellery", "Aling" })]
        [InlineData(new object[] { "Eric", "   Arts" })]
        [InlineData(new object[] { "Aminah  ", "Bonomo" })]
        public void GetAnnualDependentBenefitsCost_ValidDependentWithNameDiscount_ReturnsCorrectBenefitsInformation(string firstName, string lastName)
        {
            const decimal expectedAnnualBenefitsCost = 450.00M;
            var validPersonWithDiscount = new Person { FirstName = firstName, LastName = lastName };

            var sut = new BenefitsService();
            var result = sut.GetAnnualDependentBenefitsCost(validPersonWithDiscount);

            Assert.Equal(expectedAnnualBenefitsCost, result.AnnualCost);
            Assert.NotNull(result.Notes);
            Assert.True(result.Notes.Length > 0, "The benefits information did not contain any explanatory note");
        }
    }
}
