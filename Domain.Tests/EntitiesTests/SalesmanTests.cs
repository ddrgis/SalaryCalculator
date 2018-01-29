using System;
using System.Collections.Generic;
using Domain.Core;
using Domain.Core.Interfaces;
using NUnit.Framework;
using static TestUtils.Utils;

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
            IEmployee salesman = CreateDefaultSalesman(lengthOfWork: 4, subordinates: new List<IEmployee> {
                CreateDefaultEmployee(lengthOfWork: 5),
                CreateDefaultEmployee(lengthOfWork: 40)
            });
            IEmployee subordinateManager = CreateDefaultManager(lengthOfWork: 2, subordinates: new List<IEmployee> {
                CreateDefaultEmployee(lengthOfWork: 2),
                CreateDefaultEmployee(lengthOfWork: 2)
            });
            salesman.Subordinates.Add(subordinateManager);
            double subordinateManagerSalary = ManagerBaseSalary + (ManagerBaseSalary / 100 * ManagerIncrementForYear * 2) +
                                              (2 * (EmployeeBaseSalary + EmployeeBaseSalary / 100 * 2 * EmployeeIncrementForYear) * ManagerIncrementFromSubordinates / 100);
            double subordinateEmployeesSalary = 2 * EmployeeBaseSalary +
                                                EmployeeBaseSalary / 100 * 5 * EmployeeIncrementForYear +
                                                EmployeeBaseSalary / 100 * EmployeeIncrementForMaxYears;
            double level2SubordinatesSalary =
                2 * (EmployeeBaseSalary + EmployeeBaseSalary / 100 * 2 * EmployeeIncrementForYear);
            double allSubordinatesSalary =
                subordinateEmployeesSalary + subordinateManagerSalary + level2SubordinatesSalary;

            double salary = salesman.CountSalary(SystemTime.Now);

            Assert.AreEqual(SalesmanBaseSalary + (SalesmanBaseSalary / 100 * SalesmanIncrementForYear * 4) +
                            (allSubordinatesSalary * SalesmanIncrementFromSubordinates / 100),
                actual: salary,
                delta: 1);
        }
    }
}