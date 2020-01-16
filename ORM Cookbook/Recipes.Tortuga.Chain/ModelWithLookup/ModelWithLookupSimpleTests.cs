﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Recipes.ModelWithLookup;

namespace Recipes.Chain.ModelWithLookup
{
    [TestClass]
    public class ModelWithLookupSimpleTests : ModelWithLookupSimpleTests<EmployeeSimple>
    {
        protected override IModelWithLookupSimpleScenario<EmployeeSimple> GetScenario()
        {
            return new ModelWithLookupSimpleScenario(Setup.PrimaryDataSource);
        }
    }
}
