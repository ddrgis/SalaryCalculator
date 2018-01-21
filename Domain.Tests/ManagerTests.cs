using Domain.Core.Interfaces;
using Domain.Core.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using static Domain.Tests.TestUtils;

namespace Domain.Tests
{
    [TestFixture]
    public class ManagerTests
    {
        [Test]
        public void CountSalary_NewManagerWithFewNewEmployeeSubordinates_ReturnSalaryWithIncrement()
        {
            SystemTime.Set(new DateTime(2000, 1, 1));
            IEmployee manager = CreateDefaultManager();
            manager.Subordinates = new List<IEmployee> {
                CreateDefaultEmployee(),
                CreateDefaultEmployee()
            };

            double salary = manager.CountSalary(SystemTime.Now);

            Assert.AreEqual(ManagerBaseSalary + 2 * EmployeeBaseSalary * ManagerSubordinatesIncrement / 100, salary);
        }

        [Test]
        public void CountSalary_WithoutPayDateInput_ReturnSalaryWithIncrement()
        {
            SystemTime.Set(new DateTime(2000, 1, 1));
            IEmployee manager = CreateDefaultManager();
            manager.Subordinates = new List<IEmployee> {
                CreateDefaultEmployee(),
                CreateDefaultEmployee()
            };

            double salary = manager.CountSalary();

            Assert.AreEqual(ManagerBaseSalary + 2 * EmployeeBaseSalary * ManagerSubordinatesIncrement / 100, salary);
        }

        [Test]
        public void CountSalary_NewManagerWithEmployeeSubordinates_ReturnSalaryWithIncrement()
        {
            SystemTime.Set(new DateTime(2000, 1, 1));
            IEmployee manager = CreateDefaultManager();
            manager.Subordinates = new List<IEmployee> {
                CreateDefaultEmployee(lengthOfWork: 5),
                CreateDefaultEmployee(lengthOfWork: 40)
            };
            double subordinatesYearsIncrement = EmployeeBaseSalary / 100 * EmployeeYearIncrement * 5 +
                                                EmployeeBaseSalary / 100 * EmployeeMaxYearIncrement;

            double salary = manager.CountSalary(SystemTime.Now);

            Assert.AreEqual(ManagerBaseSalary + (2 * EmployeeBaseSalary + subordinatesYearsIncrement) * ManagerSubordinatesIncrement / 100, salary);
        }

        [Test]
        public void CountSalary_OldManagerWithTreelikeSubordinates_ReturnSalaryWithIncrement()
        {
            SystemTime.Set(new DateTime(2000, 1, 1));
            IEmployee manager = CreateDefaultManager(lengthOfWork: 5);
            manager.Subordinates = new List<IEmployee> {
                CreateDefaultEmployee(lengthOfWork: 5),
                CreateDefaultEmployee(lengthOfWork: 40)
            };
            IEmployee subordinateManager = CreateDefaultManager(lengthOfWork: 2);
            subordinateManager.Subordinates = new List<IEmployee> {
                CreateDefaultEmployee(lengthOfWork: 2),
                CreateDefaultEmployee(lengthOfWork: 2)
            };
            manager.Subordinates.Add(subordinateManager);
            double subordinateManagerSalary = ManagerBaseSalary + ManagerBaseSalary / 100 * ManagerYearIncrement * 2 +
                (2 * (EmployeeBaseSalary + EmployeeBaseSalary / 100 * 2 * EmployeeYearIncrement) * ManagerSubordinatesIncrement / 100);
            double subordinateEmployeesSalary = 2 * EmployeeBaseSalary +
                                                EmployeeBaseSalary / 100 * 5 * EmployeeYearIncrement +
                                                EmployeeBaseSalary / 100 * EmployeeMaxYearIncrement;

            double salary = manager.CountSalary(SystemTime.Now);

            Assert.AreEqual(ManagerBaseSalary + (ManagerBaseSalary / 100 * ManagerYearIncrement * 5) +
                            ((subordinateEmployeesSalary + subordinateManagerSalary) * 0.5 / 100),
                actual: salary,
                delta: 1);
        }
    }
}