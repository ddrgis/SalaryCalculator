using Domain.Core;
using Domain.Core.Entities;
using Domain.Core.Interfaces;
using Domain.Core.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Domain.Tests
{
    [TestFixture]
    public class ManagerTests
    {
        private static IEmployee CreateDefaultManager(DateTime? dateOfEmployment = null)
        {
            return new Manager(2000, dateOfEmployment ?? SystemTime.Now);
        }

        [Test]
        public void CountSalary_NewManagerWithFewNewEmployeeSubordinates_ReturnSalaryWithIncrement()
        {
            SystemTime.Set(new DateTime(2000, 1, 1));
            IEmployee manager = CreateDefaultManager();
            manager.Subordinates = new List<IEmployee> {
                    new Employee(baseSalary: 1000, dateOfEmployment: SystemTime.Now),
                    new Employee(baseSalary: 1000, dateOfEmployment: SystemTime.Now)
            };

            double salary = manager.CountSalary(SystemTime.Now);

            Assert.AreEqual(2010, salary);
        }

        [Test]
        public void CountSalary_NewManagerWithEmployeeSubordinates_ReturnSalaryWithIncrement()
        {
            SystemTime.Set(new DateTime(2000, 1, 1));
            IEmployee manager = CreateDefaultManager();
            manager.Subordinates = new List<IEmployee> {
                new Employee(baseSalary: 1000, dateOfEmployment: SystemTime.Now.AddYears(-5),
                    yearSalaryIncrement: 3, maxYearIncrement: 30),
                new Employee(baseSalary: 1000, dateOfEmployment: SystemTime.Now.AddYears(-40),
                    yearSalaryIncrement: 3, maxYearIncrement: 30)
            };

            double salary = manager.CountSalary(SystemTime.Now);

            Assert.AreEqual(2000 + (1000 + 150 + 1000 + 300) * 0.5 / 100, salary);
        }

        [Test]
        public void CountSalary_OldManagerWithEmployeeSubordinates_ReturnSalaryWithIncrement()
        {
            SystemTime.Set(new DateTime(2000, 1, 1));
            IEmployee manager = CreateDefaultManager(SystemTime.Now.AddYears(-5));
            manager.Subordinates = new List<IEmployee> {
                new Employee(baseSalary: 1000, dateOfEmployment: SystemTime.Now.AddYears(-5),
                    yearSalaryIncrement: 3, maxYearIncrement: 30),
                new Employee(baseSalary: 1000, dateOfEmployment: SystemTime.Now.AddYears(-40),
                    yearSalaryIncrement: 3, maxYearIncrement: 30)
            };
            IEmployee subordinateManager = CreateDefaultManager(SystemTime.Now.AddYears(-2));
            subordinateManager.Subordinates = new List<IEmployee> {
                new Employee(baseSalary: 1000, dateOfEmployment: SystemTime.Now.AddYears(-2),
                    yearSalaryIncrement: 3, maxYearIncrement: 30),
                new Employee(baseSalary: 1000, dateOfEmployment: SystemTime.Now.AddYears(-2),
                    yearSalaryIncrement: 3, maxYearIncrement: 30)
            };
            manager.Subordinates.Add(subordinateManager);
            const double subordinateManagerSalary = 2000 + (2 * (1000 + 1000 / 100 * 6) * 0.5 / 100);

            double salary = manager.CountSalary(SystemTime.Now);

            Assert.AreEqual(2000 + (2000 / 100 * 5 * 5) + ((1000 + 150 + 1000 + 300 + subordinateManagerSalary) * 0.5 / 100), salary, delta: 1);
        }
    }
}