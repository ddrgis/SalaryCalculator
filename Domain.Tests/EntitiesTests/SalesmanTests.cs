using NUnit.Framework;
using System;
using System.Collections.Generic;
using Domain.Core;
using Domain.Core.Interfaces;

namespace Domain.Tests.EntitiesTests
{
    [TestFixture]
    public class SalesmanTests
    {
        [TearDown]
        public void ResetAfterEachTest()
        {
            SystemTime.Reset();
        }

        [Test]
        public void CountSalary_OldSalesmanWithTreelikeSubordinates_ReturnIncrementedSalary()
        {
            SystemTime.Set(new DateTime(2000, 1, 1));
            IEmployee salesman = TestUtils.CreateDefaultSalesman(lengthOfWork: 4);
            salesman.Subordinates = new List<IEmployee> {
                TestUtils.CreateDefaultEmployee(lengthOfWork: 5),
                TestUtils.CreateDefaultEmployee(lengthOfWork: 40)
            };
            IEmployee subordinateManager = TestUtils.CreateDefaultManager(lengthOfWork: 2);
            subordinateManager.Subordinates = new List<IEmployee> {
                TestUtils.CreateDefaultEmployee(lengthOfWork: 2),
                TestUtils.CreateDefaultEmployee(lengthOfWork: 2)
            };
            salesman.Subordinates.Add(subordinateManager);
            double subordinateManagerSalary = TestUtils.ManagerBaseSalary + (TestUtils.ManagerBaseSalary / 100 * TestUtils.ManagerIncrementForYear * 2) +
                                              (2 * (TestUtils.EmployeeBaseSalary + TestUtils.EmployeeBaseSalary / 100 * 2 * TestUtils.EmployeeIncrementForYear) * TestUtils.ManagerIncrementFromSubordinates / 100);
            double subordinateEmployeesSalary = 2 * TestUtils.EmployeeBaseSalary +
                                                TestUtils.EmployeeBaseSalary / 100 * 5 * TestUtils.EmployeeIncrementForYear +
                                                TestUtils.EmployeeBaseSalary / 100 * TestUtils.EmployeeIncrementForMaxYears;
            double level2SubordinatesSalary =
                2 * (TestUtils.EmployeeBaseSalary + TestUtils.EmployeeBaseSalary / 100 * 2 * TestUtils.EmployeeIncrementForYear);
            double allSubordinatesSalary =
                subordinateEmployeesSalary + subordinateManagerSalary + level2SubordinatesSalary;

            double salary = salesman.CountSalary(SystemTime.Now);

            Assert.AreEqual(TestUtils.SalesmanBaseSalary + (TestUtils.SalesmanBaseSalary / 100 * TestUtils.SalesmanIncrementForYear * 4) +
                            (allSubordinatesSalary * TestUtils.SalesmanIncrementFromSubordinates / 100),
                actual: salary,
                delta: 1);
        }
    }
}