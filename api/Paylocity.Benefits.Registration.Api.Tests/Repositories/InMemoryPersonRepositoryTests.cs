using FluentAssertions;
using Paylocity.Benefits.Registration.Api.Exceptions;
using Paylocity.Benefits.Registration.Api.Models;
using Paylocity.Benefits.Registration.Api.Repositories;
using System;
using System.Collections.Generic;
using Xunit;

namespace Paylocity.Benefits.Registration.Api.Tests.Repositories
{
    public class InMemoryPersonRepositoryTests
    {
        [Fact]
        public void GetEmployee_NonexistentEmployee_ThrowsRepositoryException()
        {
            var emptyEmployeeStore = new Dictionary<long, Employee>();
            var idForNonexistentEmployee = long.MinValue;

            var sut = new InMemoryPersonRepository(emptyEmployeeStore, null);

            sut.Invoking(repository => repository.GetEmployee(idForNonexistentEmployee))
                .Should().Throw<NonexistentDataException>();
        }

        [Fact]
        public void GetEmployee_ValidEmployeeId_ReturnsCorrectEmployee()
        {
            var validEmployeeId = 1L;
            var expectedEmployee = new Employee
            {
                Id = validEmployeeId,
                FirstName = "Mustafa",
                LastName = "Bonham",
                AnnualSalary = 1234.56M,
                AnnualBenefitsCost = 123.45M,
                Notes = string.Empty
            };
            var employeeStore = new Dictionary<long, Employee>()
            {
                { validEmployeeId, expectedEmployee }
            };

            var sut = new InMemoryPersonRepository(employeeStore, null);

            var actualEmployee = sut.GetEmployee(validEmployeeId);

            actualEmployee.Should().BeEquivalentTo(expectedEmployee);
        }

        [Fact]
        public void StoreEmployee_NullEmployee_ThrowsArgumentNullException()
        {
            var sut = new InMemoryPersonRepository();

            sut.Invoking(repository => repository.StoreEmployee(null))
                .Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void StoreEmployee_ValidEmployee_SuccessfullyStoresEmployee()
        {
            var expectedFirstName = "Apostolis";
            var expectedLastName = "Falco";
            var expectedAnnualSalary = 24689.00M;
            var expectedAnnualBenefitCost = 388.00M;
            var expectedNotes = "something";
            var employeeInfo = new EmployeeInfo
            {
                FirstName = expectedFirstName,
                LastName = expectedLastName,
                AnnualSalary = expectedAnnualSalary,
                AnnualBenefitsCost = expectedAnnualBenefitCost,
                Notes = expectedNotes
            };
            var employeeStore = new Dictionary<long, Employee>();

            var sut = new InMemoryPersonRepository(employeeStore, null);

            var actualEmployee = sut.StoreEmployee(employeeInfo);

            using (new FluentAssertions.Execution.AssertionScope())
            {
                actualEmployee.Should().BeEquivalentTo(employeeInfo);
                employeeStore.ContainsKey(actualEmployee.Id).Should().BeTrue();
                employeeStore[actualEmployee.Id].Should().BeSameAs(actualEmployee);
            }
        }

        [Fact]
        public void StoreDependent_NullEmployee_ThrowsArgumentNullException()
        {
            var sut = new InMemoryPersonRepository();

            sut.Invoking(repository => repository.StoreDependent(null))
                .Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void StoreDependent_ValidDependent_SuccessfullyStoresDependent()
        {
            var expectedEmployeeId = 1L;
            var expectedFirstName = "Theodosia";
            var expectedLastName = "Kuang";
            var expectedAnnualBenefitCost = 753.00M;
            var expectedNotes = "Some more notes";

            var dependentInfo = new DependentInfo
            {
                EmployeeId = expectedEmployeeId,
                FirstName = expectedFirstName,
                LastName = expectedLastName,
                AnnualBenefitsCost = expectedAnnualBenefitCost,
                Notes = expectedNotes
            };
            var dependentStore = new Dictionary<long, Dependent>();

            var sut = new InMemoryPersonRepository(null, dependentStore);

            var actualDependent = sut.StoreDependent(dependentInfo);

            using (new FluentAssertions.Execution.AssertionScope())
            {
                actualDependent.Should().BeEquivalentTo(dependentInfo);
                dependentStore.ContainsKey(actualDependent.Id).Should().BeTrue();
                dependentStore[actualDependent.Id].Should().BeSameAs(actualDependent);
            }
        }

        [Fact]
        public void GetEmployeeDependents_EmployeeWithNoDependents_ReturnsEmptyResult()
        {
            var irrelevantEmployeeId = 0L;
            var emptyDependentStore = new Dictionary<long, Dependent>();

            var sut = new InMemoryPersonRepository(null, emptyDependentStore);

            var actualDependents = sut.GetEmployeeDependents(irrelevantEmployeeId);

            actualDependents.Should().BeEmpty();
        }

        [Fact]
        public void GetEmployeeDependents_ValidEmployeeId_ReturnsCorrectDependents()
        {
            var employeeWithDependentsId = 1L;
            var someOtherEmployeeId = 2L;
            var expectedDependents = new List<Dependent>
            {
                new Dependent { Id = 1L, EmployeeId = employeeWithDependentsId, FirstName = "Nkosana", LastName = "Raskob", AnnualBenefitsCost = 123.45M, Notes = "something" },
                new Dependent { Id = 2L, EmployeeId = employeeWithDependentsId, FirstName = "Shahrazad", LastName = "Bach", AnnualBenefitsCost = 200.00M, Notes = "something else" }
            };
            var dependentsForSomeOtherEmployer = new List<Dependent>
            {
                new Dependent { Id = 3L, EmployeeId = someOtherEmployeeId, FirstName = "Tao", LastName = "Langlais", AnnualBenefitsCost = 111.00M, Notes = "Yet more notes" },
                new Dependent { Id = 4L, EmployeeId = someOtherEmployeeId, FirstName = "Gabino", LastName = "Bruhn", AnnualBenefitsCost = 100.00M, Notes = "Blah blah blah" }
            };

            var dependentStore = new Dictionary<long, Dependent>();
            expectedDependents.ForEach(dependent => dependentStore[dependent.Id] = dependent);
            dependentsForSomeOtherEmployer.ForEach(dependent => dependentStore[dependent.Id] = dependent);

            var sut = new InMemoryPersonRepository(null, dependentStore);

            var actualDependents = sut.GetEmployeeDependents(employeeWithDependentsId);

            actualDependents.Should().BeEquivalentTo(expectedDependents);
        }
    }
}
