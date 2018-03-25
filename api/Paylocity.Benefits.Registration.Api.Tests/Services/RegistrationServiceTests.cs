using FluentAssertions;
using Moq;
using Paylocity.Benefits.Registration.Api.Exceptions;
using Paylocity.Benefits.Registration.Api.Interfaces;
using Paylocity.Benefits.Registration.Api.Models;
using Paylocity.Benefits.Registration.Api.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace Paylocity.Benefits.Registration.Api.Tests.Services
{
    public class RegistrationServiceTests
    {
        [Fact]
        public void RegisterEmployee_NullPerson_ThrowsArgumentNullException()
        {
            var mockRepository = new Mock<IPersonRepository>();
            var mockCompensationService = new Mock<ICompensationService>();
            var mockBenefitsService = new Mock<IBenefitsService>();
            var mockPaymentCalculator = new Mock<IPaymentCalculator>();

            var sut = new RegistrationService(
                mockRepository.Object,
                mockCompensationService.Object,
                mockBenefitsService.Object,
                mockPaymentCalculator.Object);

            sut.Invoking(registrationService => registrationService.RegisterEmployee(null))
                .Should().Throw<ArgumentNullException>();
        }


        [Theory]
        [InlineData(new object[] { null, "Bousaid" })]
        [InlineData(new object[] { "Sylvianne", null })]
        [InlineData(new object[] { "", "Tolkien" })]
        [InlineData(new object[] { "Deborah", "" })]
        public void RegisterEmployee_PersonWithInvalidName_ThrowsArgumentException(string questionableFirstName, string questionableLastName)
        {
            var invalidPerson = new Person
            {
                FirstName = questionableFirstName,
                LastName = questionableLastName
            };

            var mockRepository = new Mock<IPersonRepository>();
            var mockCompensationService = new Mock<ICompensationService>();
            var mockBenefitsService = new Mock<IBenefitsService>();
            var mockPaymentCalculator = new Mock<IPaymentCalculator>();

            var sut = new RegistrationService(
                mockRepository.Object,
                mockCompensationService.Object,
                mockBenefitsService.Object,
                mockPaymentCalculator.Object);

            sut.Invoking(registrationService => registrationService.RegisterEmployee(invalidPerson))
                .Should().Throw<ArgumentException>();
        }

        [Fact]
        public void RegisterEmployee_ValidPerson_SuccessfullyRegistersEmployee()
        {
            var expectedId = 1L;
            var expectedFirstName = "Akbar";
            var expectedLastName = "Walther";
            var expectedAnnualSalary = 123456.78M;
            var expectedAnnualBenefitsCost = 2468.10M;
            var expectedNotes = "Some notes";

            var validPerson = new Person
            {
                FirstName = expectedFirstName,
                LastName = expectedLastName
            };

            var expectedEmployee = new Employee
            {
                Id = expectedId,
                FirstName = expectedFirstName,
                LastName = expectedLastName,
                AnnualSalary = expectedAnnualSalary,
                AnnualBenefitsCost = expectedAnnualBenefitsCost,
                Notes = expectedNotes
            };

            var mockRepository = new Mock<IPersonRepository>();
            var mockCompensationService = new Mock<ICompensationService>();
            var mockBenefitsService = new Mock<IBenefitsService>();
            var mockPaymentCalculator = new Mock<IPaymentCalculator>();

            mockCompensationService.Setup(service => service.GetAnnualSalary(validPerson))
                .Returns(expectedAnnualSalary);

            mockBenefitsService.Setup(service => service.GetAnnualEmployeeBenefitsCost(validPerson))
                .Returns(new BenefitsInfo { AnnualCost = expectedAnnualBenefitsCost, Notes = expectedNotes });

            mockRepository.Setup(repository => repository.StoreEmployee(It.Is<EmployeeInfo>(employeeInfo =>
                (employeeInfo.FirstName == expectedFirstName) &&
                (employeeInfo.LastName == expectedLastName) &&
                (employeeInfo.AnnualSalary == expectedAnnualSalary) &&
                (employeeInfo.AnnualBenefitsCost == expectedAnnualBenefitsCost) &&
                (employeeInfo.Notes == expectedNotes))))
                .Returns(expectedEmployee);

            var sut = new RegistrationService(
                mockRepository.Object,
                mockCompensationService.Object,
                mockBenefitsService.Object,
                mockPaymentCalculator.Object);

            var actualEmployee = sut.RegisterEmployee(validPerson);

            mockCompensationService.VerifyAll();
            actualEmployee.Should().Be(expectedEmployee);
        }

        [Fact]
        public void RegisterDependent_NullPerson_ThrowsArgumentNullException()
        {
            var irrelevantEmployeeId = 0L;

            var mockRepository = new Mock<IPersonRepository>();
            var mockCompensationService = new Mock<ICompensationService>();
            var mockBenefitsService = new Mock<IBenefitsService>();
            var mockPaymentCalculator = new Mock<IPaymentCalculator>();

            var sut = new RegistrationService(
                mockRepository.Object,
                mockCompensationService.Object,
                mockBenefitsService.Object,
                mockPaymentCalculator.Object);

            sut.Invoking(registrationService => registrationService.RegisterDependent(irrelevantEmployeeId, null))
                .Should().Throw<ArgumentNullException>();
        }


        [Theory]
        [InlineData(new object[] { null, "Leitzke" })]
        [InlineData(new object[] { "Yuliy", null })]
        [InlineData(new object[] { "", "Espenson" })]
        [InlineData(new object[] { "Hikari", "" })]
        public void RegisterDependent_PersonWithInvalidName_ThrowsArgumentException(string questionableFirstName, string questionableLastName)
        {
            var irrelevantEmployeeId = 0L;
            var invalidPerson = new Person
            {
                FirstName = questionableFirstName,
                LastName = questionableLastName
            };

            var mockRepository = new Mock<IPersonRepository>();
            var mockCompensationService = new Mock<ICompensationService>();
            var mockBenefitsService = new Mock<IBenefitsService>();
            var mockPaymentCalculator = new Mock<IPaymentCalculator>();

            var sut = new RegistrationService(
                mockRepository.Object,
                mockCompensationService.Object,
                mockBenefitsService.Object,
                mockPaymentCalculator.Object);

            sut.Invoking(registrationService => registrationService.RegisterDependent(irrelevantEmployeeId, invalidPerson))
                .Should().Throw<ArgumentException>();
        }

        [Fact]
        public void RegisterDependent_ValidPerson_SuccessfullyRegistersDependent()
        {
            var expectedId = 2L;
            var expectedEmployeeId = 1L;
            var expectedFirstName = "Audhild";
            var expectedLastName = "Lorenz";
            var expectedAnnualBenefitsCost = 6464.00M;
            var expectedNotes = "Yet more notes";

            var validPerson = new Person
            {
                FirstName = expectedFirstName,
                LastName = expectedLastName
            };

            var expectedDependent = new Dependent
            {
                Id = expectedId,
                EmployeeId = expectedEmployeeId,
                FirstName = expectedFirstName,
                LastName = expectedLastName,
                AnnualBenefitsCost = expectedAnnualBenefitsCost,
                Notes = expectedNotes
            };

            var mockRepository = new Mock<IPersonRepository>();
            var mockCompensationService = new Mock<ICompensationService>();
            var mockBenefitsService = new Mock<IBenefitsService>();
            var mockPaymentCalculator = new Mock<IPaymentCalculator>();

            mockBenefitsService.Setup(service => service.GetAnnualDependentBenefitsCost(validPerson))
                .Returns(new BenefitsInfo { AnnualCost = expectedAnnualBenefitsCost, Notes = expectedNotes });

            mockRepository.Setup(repository => repository.StoreDependent(It.Is<DependentInfo>(dependentInfo =>
                (dependentInfo.EmployeeId == expectedEmployeeId) &&
                (dependentInfo.FirstName == expectedFirstName) &&
                (dependentInfo.LastName == expectedLastName) &&
                (dependentInfo.AnnualBenefitsCost == expectedAnnualBenefitsCost) &&
                (dependentInfo.Notes == expectedNotes))))
                .Returns(expectedDependent);

            var sut = new RegistrationService(
                mockRepository.Object,
                mockCompensationService.Object,
                mockBenefitsService.Object,
                mockPaymentCalculator.Object);

            var actualDependent = sut.RegisterDependent(expectedEmployeeId, validPerson);

            mockCompensationService.VerifyAll();
            actualDependent.Should().Be(expectedDependent);
        }

        [Fact]
        public void PreviewPayPeriods_NonexistentEmployee_PropagatesException()
        {
            const long nonexistentEmployeeId = 0L;

            var mockRepository = new Mock<IPersonRepository>();
            var mockCompensationService = new Mock<ICompensationService>();
            var mockBenefitsService = new Mock<IBenefitsService>();
            var mockPaymentCalculator = new Mock<IPaymentCalculator>();

            mockRepository.Setup(
                repository => repository.GetEmployee(nonexistentEmployeeId))
                .Throws(new NonexistentDataException(nonexistentEmployeeId.ToString()));

            var sut = new RegistrationService(
                mockRepository.Object,
                mockCompensationService.Object,
                mockBenefitsService.Object,
                mockPaymentCalculator.Object);

            sut.Invoking(registrationService => registrationService.PreviewPayPeriods(nonexistentEmployeeId))
                .Should().Throw<InvalidRequestException>();
        }

        [Fact]
        public void PreviewPayPeriods_EmployeeOnly_ReturnsCorrectPayPeriods()
        {
            const long validEmployeeId = 1L;
            const decimal expectedAnnualSalary = 50000.00M;
            const decimal expectedAnnualBenefitsCost = 1000.00M;
            var payments = new decimal[DemoConstants.NUMBER_OF_PAY_PERIODS_PER_YEAR];


            var mockRepository = new Mock<IPersonRepository>();
            var mockCompensationService = new Mock<ICompensationService>();
            var mockBenefitsService = new Mock<IBenefitsService>();
            var mockPaymentCalculator = new Mock<IPaymentCalculator>();

            mockRepository.Setup(
                repository => repository.GetEmployee(validEmployeeId))
                .Returns(new Employee { Id = validEmployeeId, AnnualSalary = expectedAnnualSalary, AnnualBenefitsCost = expectedAnnualBenefitsCost });

            mockRepository.Setup(
                repository => repository.GetEmployeeDependents(validEmployeeId))
                .Returns(new List<Dependent>());

            mockPaymentCalculator.Setup(calculator => calculator.CalculatePayments(expectedAnnualSalary, DemoConstants.NUMBER_OF_PAY_PERIODS_PER_YEAR))
                .Returns(payments);

            mockPaymentCalculator.Setup(calculator => calculator.CalculatePayments(expectedAnnualBenefitsCost, DemoConstants.NUMBER_OF_PAY_PERIODS_PER_YEAR))
                .Returns(payments);

            var sut = new RegistrationService(
                mockRepository.Object,
                mockCompensationService.Object,
                mockBenefitsService.Object,
                mockPaymentCalculator.Object);

            var actualPayPeriods = sut.PreviewPayPeriods(validEmployeeId);

            mockRepository.VerifyAll();
            mockPaymentCalculator.VerifyAll();
            actualPayPeriods.Should().NotBeNullOrEmpty()
                .And.HaveCount(DemoConstants.NUMBER_OF_PAY_PERIODS_PER_YEAR);
        }

        [Fact]
        public void PreviewPayPeriods_EmployeeAndDependents_ReturnsCorrectPayPeriods()
        {
            const long validEmployeeId = 1L;
            const decimal expectedAnnualSalary = 50000.00M;
            const decimal expectedEmployeeAnnualBenefitsCost = 1000.00M;
            const decimal expectedDependentAnnualBenefitsCost = 500.00M;

            var dependents = new List<Dependent>
            {
                new Dependent { AnnualBenefitsCost = expectedDependentAnnualBenefitsCost },
                new Dependent { AnnualBenefitsCost = expectedDependentAnnualBenefitsCost },
                new Dependent { AnnualBenefitsCost = expectedDependentAnnualBenefitsCost }
            };

            var expectedAnnualBenefitsCost = expectedEmployeeAnnualBenefitsCost + dependents.Count * expectedDependentAnnualBenefitsCost;

            var payments = new decimal[DemoConstants.NUMBER_OF_PAY_PERIODS_PER_YEAR];


            var mockRepository = new Mock<IPersonRepository>();
            var mockCompensationService = new Mock<ICompensationService>();
            var mockBenefitsService = new Mock<IBenefitsService>();
            var mockPaymentCalculator = new Mock<IPaymentCalculator>();

            mockRepository.Setup(
                repository => repository.GetEmployee(validEmployeeId))
                .Returns(new Employee { Id = validEmployeeId, AnnualSalary = expectedAnnualSalary, AnnualBenefitsCost = expectedEmployeeAnnualBenefitsCost });

            mockRepository.Setup(
                repository => repository.GetEmployeeDependents(validEmployeeId))
                .Returns(dependents);

            mockPaymentCalculator.Setup(calculator => calculator.CalculatePayments(expectedAnnualSalary, DemoConstants.NUMBER_OF_PAY_PERIODS_PER_YEAR))
                .Returns(payments);

            mockPaymentCalculator.Setup(calculator => calculator.CalculatePayments(expectedAnnualBenefitsCost, DemoConstants.NUMBER_OF_PAY_PERIODS_PER_YEAR))
                .Returns(payments);

            var sut = new RegistrationService(
                mockRepository.Object,
                mockCompensationService.Object,
                mockBenefitsService.Object,
                mockPaymentCalculator.Object);

            var actualPayPeriods = sut.PreviewPayPeriods(validEmployeeId);

            mockRepository.VerifyAll();
            mockPaymentCalculator.VerifyAll();
            actualPayPeriods.Should().NotBeNullOrEmpty()
                .And.HaveCount(DemoConstants.NUMBER_OF_PAY_PERIODS_PER_YEAR);
        }
    }
}
