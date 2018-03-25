using Paylocity.Benefits.Registration.Api.Models;
using System;
using System.Collections.Generic;

namespace Paylocity.Benefits.Registration.IntegrationTests
{
    public static class TestDataStores
    {
        public static readonly IDictionary<long, Employee> EmployeeStore = new Dictionary<long, Employee>();
        public static readonly IDictionary<long, Dependent> DependentStore = new Dictionary<long, Dependent>();

        public class TestDataStoreScope : IDisposable
        {
            public void Dispose()
            {
                TestDataStores.EmployeeStore.Clear();
                TestDataStores.DependentStore.Clear();
            }
        }

        public static IDisposable Scope()
        {
            TestDataStores.EmployeeStore.Clear();
            TestDataStores.DependentStore.Clear();
            return new TestDataStores.TestDataStoreScope();
        }
    }
}
