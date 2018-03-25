using FluentAssertions;
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

            sut.Invoking(benefitsService => benefitsService.GetAnnualEmployeeBenefitsCost(null))
                .Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(new object[] {  (string)null, "Smith" })]
        [InlineData(new object[] { "", "Nakamura" })]
        [InlineData(new object[] { "Ravi", "   " })]
        public void GetAnnualEmployeeBenefitsCost_InvalidEmployee_ThrowsArgumentException(string firstName, string lastName)
        {
            var invalidPerson = new Person { FirstName = firstName, LastName = lastName };

            var sut = new BenefitsService();

            sut.Invoking(benefitsService => benefitsService.GetAnnualEmployeeBenefitsCost(invalidPerson))
                .Should().Throw<ArgumentException>();
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

            result.Should().NotBeNull();

            using (new FluentAssertions.Execution.AssertionScope())
            {
                result.AnnualCost.Should().Be(expectedAnnualBenefitsCost);
                result.Notes.Should().BeEmpty("neither the person's first nor last names starts with 'a', so they don't get a discount and there's no need for a note.");
            }
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

            result.Should().NotBeNull();

            using (new FluentAssertions.Execution.AssertionScope())
            {
                result.AnnualCost.Should().Be(expectedAnnualBenefitsCost);
                result.Notes.Should().NotBeNullOrEmpty("the person's first or last name started with the letter 'A', entitling them to a discount, which warrants an explanatory note.");
            }
        }

        [Fact]
        public void GetAnnualDependentBenefitsCost_NullDependent_ThrowsArgumentNullException()
        {
            var sut = new BenefitsService();

            sut.Invoking(benefitsService => benefitsService.GetAnnualDependentBenefitsCost(null))
                .Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(new object[] { (string)null, "Ebrahimi" })]
        [InlineData(new object[] { "", "Devin" })]
        [InlineData(new object[] { "Zakiah", "   " })]
        public void GetAnnualDependentBenefitsCost_InvalidDependent_ThrowsArgumentException(string firstName, string lastName)
        {
            var invalidPerson = new Person { FirstName = firstName, LastName = lastName };

            var sut = new BenefitsService();

            sut.Invoking(benefitsService => benefitsService.GetAnnualDependentBenefitsCost(invalidPerson))
                .Should().Throw<ArgumentException>();
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

            result.Should().NotBeNull();

            using (new FluentAssertions.Execution.AssertionScope())
            {
                result.AnnualCost.Should().Be(expectedAnnualBenefitsCost);
                result.Notes.Should().BeEmpty("neither the person's first nor last names starts with 'a', so they don't get a discount and there's no need for a note.");
            }
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

            result.Should().NotBeNull();

            using (new FluentAssertions.Execution.AssertionScope())
            {
                result.AnnualCost.Should().Be(expectedAnnualBenefitsCost);
                result.Notes.Should().NotBeNullOrEmpty("the person's first or last name started with the letter 'A', entitling them to a discount, which warrants an explanatory note.");
            }
        }
    }
}
