using Domain.Core.Interfaces;
using Domain.Core.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using static Domain.Tests.EntitiesTests.TestUtils;

namespace Domain.Tests
{
    [TestFixture]
    public class SalesmanTests
    {
        [Test]
        public void CountSalary_OldSalesmanWithTreelikeSubordinates_ReturnSalaryWithIncrement()
        {
            SystemTime.Set(new DateTime(2000, 1, 1));
            IEmployee salesman = CreateDefaultSalesman(lengthOfWork: 4);
            salesman.Subordinates = new List<IEmployee> {
                CreateDefaultEmployee(lengthOfWork: 5),
                CreateDefaultEmployee(lengthOfWork: 40)
            };
            IEmployee subordinateManager = CreateDefaultManager(lengthOfWork: 2);
            subordinateManager.Subordinates = new List<IEmployee> {
                CreateDefaultEmployee(lengthOfWork: 2),
                CreateDefaultEmployee(lengthOfWork: 2)
            };
            salesman.Subordinates.Add(subordinateManager);
            double subordinateManagerSalary = ManagerBaseSalary + (ManagerBaseSalary / 100 * ManagerYearIncrement * 2) +
                                              (2 * (EmployeeBaseSalary + EmployeeBaseSalary / 100 * 2 * EmployeeYearIncrement) * ManagerSubordinatesIncrement / 100);
            double subordinateEmployeesSalary = 2 * EmployeeBaseSalary +
                                                EmployeeBaseSalary / 100 * 5 * EmployeeYearIncrement +
                                                EmployeeBaseSalary / 100 * EmployeeMaxYearIncrement;
            double level2SubordinatesSalary =
                2 * (EmployeeBaseSalary + EmployeeBaseSalary / 100 * 2 * EmployeeYearIncrement);
            double allSubordinatesSalary =
                subordinateEmployeesSalary + subordinateManagerSalary + level2SubordinatesSalary;

            double salary = salesman.CountSalary(SystemTime.Now);

            Assert.AreEqual(SalesmanBaseSalary + (SalesmanBaseSalary / 100 * SalesmanYearIncrement * 4) +
                            (allSubordinatesSalary * SalesmanSubordinatesIncrement / 100),
                actual: salary,
                delta: 1);
        }
    }
}