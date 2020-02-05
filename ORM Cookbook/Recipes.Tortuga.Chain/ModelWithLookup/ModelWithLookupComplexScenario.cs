﻿using Recipes.Chain.Models;
using Recipes.ModelWithLookup;
using System;
using System.Collections.Generic;
using Tortuga.Chain;

namespace Recipes.Chain.ModelWithLookup
{
    public class ModelWithLookupComplexScenario : IModelWithLookupComplexScenario<EmployeeComplex>
    {
        const string ClassificationTableName = "HR.EmployeeClassification";
        const string WriteTableName = "HR.Employee";

        readonly SqlServerDataSource m_DataSource;

        public ModelWithLookupComplexScenario(SqlServerDataSource dataSource)
        {
            m_DataSource = dataSource;
        }

        public int Create(EmployeeComplex employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee), $"{nameof(employee)} is null.");

            //The object is mapped to the view, so we need to override the table we write to.
            return m_DataSource.Insert(WriteTableName, employee).ToInt32().Execute();
        }

        public void Delete(EmployeeComplex employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee), $"{nameof(employee)} is null.");

            //The object is mapped to the view, so we need to override the table we write to.
            m_DataSource.Delete(WriteTableName, employee).Execute();
        }

        public void DeleteByKey(int employeeKey)
        {
            m_DataSource.DeleteByKey(WriteTableName, employeeKey).Execute();
        }

        public IList<EmployeeComplex> FindByLastName(string lastName)
        {
            return m_DataSource.From<EmployeeComplex>(new { LastName = lastName }).ToCollection().Execute();
        }

        public IList<EmployeeComplex> GetAll()
        {
            return m_DataSource.From<EmployeeComplex>().ToCollection().Execute();
        }

        public EmployeeComplex? GetByKey(int employeeKey)
        {
            return m_DataSource.From<EmployeeComplex>(new { employeeKey }).ToObjectOrNull().Execute();
        }

        public IEmployeeClassification? GetClassification(int employeeClassificationKey)
        {
            return m_DataSource.GetByKey(ClassificationTableName, employeeClassificationKey).ToObject<EmployeeClassification>().Execute();
        }

        public void Update(EmployeeComplex employee)
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee), $"{nameof(employee)} is null.");

            //The object is mapped to the view, so we need to override the table we write to.
            m_DataSource.Update(WriteTableName, employee).Execute();
        }
    }
}
