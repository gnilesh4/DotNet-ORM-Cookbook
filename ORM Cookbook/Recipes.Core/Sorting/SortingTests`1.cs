﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Recipes.Sorting
{
    /// <summary>
    /// This scenario performs basic CRUD operations on a model containing a foreign key represented by an integer.
    /// </summary>
    /// <typeparam name="TModel">An Employee model or entity</typeparam>
    [TestCategory("Sorting")]
    public abstract class SortingTests<TModel> : TestBase
    where TModel : class, IEmployeeSimple, new()
    {
        protected abstract ISortingScenario<TModel> GetScenario();

        [TestMethod]
        public void SortByLastName()
        {
            var repository = GetScenario();

            //Ensure some records exist
            CreateEmployees(repository);

            var results = repository.SortByLastName();
            for (var i = 1; i < results.Count; i++)
            {
                Assert.IsTrue(string.Compare(results[i - 1].LastName, results[i].LastName, StringComparison.OrdinalIgnoreCase) <= 0);
            }
        }

        [TestMethod]
        public void SortByLastNameFirstName()
        {
            var repository = GetScenario();

            //Ensure some records exist
            CreateEmployees(repository);

            var results = repository.SortByLastNameFirstName();
            for (var i = 1; i < results.Count; i++)
            {
                Assert.IsTrue(string.Compare(results[i - 1].LastName, results[i].LastName, StringComparison.OrdinalIgnoreCase) <= 0);
                if (string.Equals(results[i - 1].LastName, results[i].LastName, StringComparison.OrdinalIgnoreCase))
                {
                    Assert.IsTrue(string.Compare(results[i - 1].FirstName, results[i].FirstName, StringComparison.OrdinalIgnoreCase) <= 0);
                }
            }
        }

        [TestMethod]
        public void SortByLastNameDescFirstName()
        {
            var repository = GetScenario();

            //Ensure some records exist
            CreateEmployees(repository);

            var results = repository.SortByLastNameDescFirstName();
            for (var i = 1; i < results.Count; i++)
            {
                Assert.IsTrue(string.Compare(results[i - 1].LastName, results[i].LastName, StringComparison.OrdinalIgnoreCase) >= 0);
                if (string.Equals(results[i - 1].LastName, results[i].LastName, StringComparison.OrdinalIgnoreCase))
                {
                    Assert.IsTrue(string.Compare(results[i - 1].FirstName, results[i].FirstName, StringComparison.OrdinalIgnoreCase) <= 0);
                }
            }
        }

        static void CreateEmployees(ISortingScenario<TModel> repository)
        {
            long ticks = DateTime.Now.Ticks;

            for (var i = 1; i < 6; i++)
            {
                var newRecord = new TModel
                {
                    FirstName = "Test " + (i % 3),
                    MiddleName = "A" + i,
                    LastName = "Person " + ticks,
                    EmployeeClassificationKey = i
                };
                var newKey = repository.Create(newRecord);
                Assert.IsTrue(newKey >= 1000, "keys under 1000 were not generated by the database");
            }
        }
    }
}
