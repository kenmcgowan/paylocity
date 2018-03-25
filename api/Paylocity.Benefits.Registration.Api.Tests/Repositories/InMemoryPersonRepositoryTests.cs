using Paylocity.Benefits.Registration.Api.Exceptions;
using Paylocity.Benefits.Registration.Api.Models;
using Paylocity.Benefits.Registration.Api.Repositories;
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

            Assert.Throws<NonexistentDataException>(() => sut.GetEmployee(idForNonexistentEmployee));
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

            Assert.Equal(expectedEmployee.Id, actualEmployee.Id);
            Assert.Equal(expectedEmployee.FirstName, actualEmployee.FirstName);
            Assert.Equal(expectedEmployee.LastName, actualEmployee.LastName);
            Assert.Equal(expectedEmployee.AnnualSalary, actualEmployee.AnnualSalary);
            Assert.Equal(expectedEmployee.AnnualBenefitsCost, actualEmployee.AnnualBenefitsCost);
            Assert.Equal(expectedEmployee.Notes, actualEmployee.Notes);
        }

        [Fact]
        public void StoreEmployee_ValidEmployee_SuccessfullyStoresEmployee()
        {
            var employeeStore = new Dictionary<long, Employee>();
            var expectedFirstName = "Apostolis";
            var expectedLastName = "Falco";
            var expectedAnnualSalary = 24689.00M;
            var expectedAnnualBenefitCost = 388.00M;
            var expectedNotes = "something";

            var sut = new InMemoryPersonRepository(employeeStore, null);

            var actualEmployee = sut.StoreEmployee(
                expectedFirstName,
                expectedLastName,
                expectedAnnualSalary,
                expectedAnnualBenefitCost,
                expectedNotes);

            Assert.Equal(expectedFirstName, actualEmployee.FirstName);
            Assert.Equal(expectedLastName, actualEmployee.LastName);
            Assert.Equal(expectedAnnualSalary, actualEmployee.AnnualSalary);
            Assert.Equal(expectedAnnualBenefitCost, actualEmployee.AnnualBenefitsCost);
            Assert.Equal(expectedNotes, actualEmployee.Notes);

            Assert.Contains(actualEmployee, employeeStore.Values);
        }

        [Fact]
        public void StoreDependent_ValidDependent_SuccessfullyStoresDependent()
        {
            var dependentStore = new Dictionary<long, Dependent>();
            var expectedEmployeeId = 1L;
            var expectedFirstName = "Theodosia";
            var expectedLastName = "Kuang";
            var expectedAnnualBenefitCost = 753.00M;
            var expectedNotes = "Some more notes";

            var sut = new InMemoryPersonRepository(null, dependentStore);

            var actualDependent = sut.StoreDependent(
                expectedEmployeeId,
                expectedFirstName,
                expectedLastName,
                expectedAnnualBenefitCost,
                expectedNotes);

            Assert.Equal(expectedEmployeeId, actualDependent.EmployeeId);
            Assert.Equal(expectedFirstName, actualDependent.FirstName);
            Assert.Equal(expectedLastName, actualDependent.LastName);
            Assert.Equal(expectedAnnualBenefitCost, actualDependent.AnnualBenefitsCost);
            Assert.Equal(expectedNotes, actualDependent.Notes);

            Assert.Contains(actualDependent, dependentStore.Values);
        }

        [Fact]
        public void GetEmployeeDependents_EmployeeWithNoDependents_ReturnsEmptyResult()
        {
            var irrelevantEmployeeId = 0L;
            var emptyDependentStore = new Dictionary<long, Dependent>();

            var sut = new InMemoryPersonRepository(null, emptyDependentStore);

            var actualDependents = sut.GetEmployeeDependents(irrelevantEmployeeId);

            Assert.Empty(actualDependents);
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
            var otherDependents = new List<Dependent>
            {
                new Dependent { Id = 3L, EmployeeId = someOtherEmployeeId, FirstName = "Tao", LastName = "Langlais", AnnualBenefitsCost = 111.00M, Notes = "Yet more notes" },
                new Dependent { Id = 4L, EmployeeId = someOtherEmployeeId, FirstName = "Gabino", LastName = "Bruhn", AnnualBenefitsCost = 100.00M, Notes = "Blah blah blah" }
            };

            var dependentstore = new Dictionary<long, Dependent>();
            expectedDependents.ForEach(dependent => dependentstore[dependent.Id] = dependent);
            otherDependents.ForEach(dependent => dependentstore[dependent.Id] = dependent);

            var sut = new InMemoryPersonRepository(null, dependentstore);

            var actualDependents = sut.GetEmployeeDependents(employeeWithDependentsId);

            Assert.Equal(expectedDependents, actualDependents);
        }
    }
}
