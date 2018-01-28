using NUnit.Framework;
using System;
using System.Collections.Generic;
using Domain.Core;
using Domain.Core.Interfaces;

namespace Domain.Tests.EntitiesTests
{
    [TestFixture]
    public class ManagerTests
    {
        [TearDown]
        public void ResetAfterEachTest()
        {
            SystemTime.Reset();
        }

        [Test]
        public void CountSalary_NewManagerWithFewNewEmployeeSubordinates_ReturnIncrementedSalary()
        {
            SystemTime.Set(new DateTime(2000, 1, 1));
            IEmployee manager = TestUtils.CreateDefaultManager();
            manager.Subordinates = new List<IEmployee> {
                TestUtils.CreateDefaultEmployee(),
                TestUtils.CreateDefaultEmployee()
            };

            double salary = manager.CountSalary(SystemTime.Now);

            Assert.AreEqual(TestUtils.ManagerBaseSalary + 2 * TestUtils.EmployeeBaseSalary * TestUtils.ManagerIncrementFromSubordinates / 100, salary);
        }

        [Test]
        public void CountSalary_WithoutPayDateInput_ReturnIncrementedSalary()
        {
            SystemTime.Set(new DateTime(2000, 1, 1));
            IEmployee manager = TestUtils.CreateDefaultManager();
            manager.Subordinates = new List<IEmployee> {
                TestUtils.CreateDefaultEmployee(),
                TestUtils.CreateDefaultEmployee()
            };

            double salary = manager.CountSalary();

            Assert.AreEqual(TestUtils.ManagerBaseSalary + 2 * TestUtils.EmployeeBaseSalary * TestUtils.ManagerIncrementFromSubordinates / 100, salary);
        }

        [Test]
        public void CountSalary_NewManagerWithEmployeeSubordinates_ReturnIncrementedSalary()
        {
            SystemTime.Set(new DateTime(2000, 1, 1));
            IEmployee manager = TestUtils.CreateDefaultManager();
            manager.Subordinates = new List<IEmployee> {
                TestUtils.CreateDefaultEmployee(lengthOfWork: 5),
                TestUtils.CreateDefaultEmployee(lengthOfWork: 40)
            };
            double subordinatesIncrementForYears = TestUtils.EmployeeBaseSalary / 100 * TestUtils.EmployeeIncrementForYear * 5 +
                                                TestUtils.EmployeeBaseSalary / 100 * TestUtils.EmployeeIncrementForMaxYears;

            double salary = manager.CountSalary(SystemTime.Now);

            Assert.AreEqual(TestUtils.ManagerBaseSalary +
                            (2 * TestUtils.EmployeeBaseSalary + subordinatesIncrementForYears) * TestUtils.ManagerIncrementFromSubordinates / 100, salary);
        }

        [Test]
        public void CountSalary_OldManagerWithTreelikeSubordinates_ReturnIncrementedSalary()
        {
            SystemTime.Set(new DateTime(2000, 1, 1));
            IEmployee manager = TestUtils.CreateDefaultManager(lengthOfWork: 5);
            manager.Subordinates = new List<IEmployee> {
                TestUtils.CreateDefaultEmployee(lengthOfWork: 5),
                TestUtils.CreateDefaultEmployee(lengthOfWork: 40)
            };
            IEmployee subordinateManager = TestUtils.CreateDefaultManager(lengthOfWork: 2);
            subordinateManager.Subordinates = new List<IEmployee> {
                TestUtils.CreateDefaultEmployee(lengthOfWork: 2),
                TestUtils.CreateDefaultEmployee(lengthOfWork: 2)
            };
            manager.Subordinates.Add(subordinateManager);
            double subordinateManagerSalary = TestUtils.ManagerBaseSalary + TestUtils.ManagerBaseSalary / 100 * TestUtils.ManagerIncrementForYear * 2 +
                (2 * (TestUtils.EmployeeBaseSalary + TestUtils.EmployeeBaseSalary / 100 * 2 * TestUtils.EmployeeIncrementForYear) * TestUtils.ManagerIncrementFromSubordinates / 100);
            double subordinateEmployeesSalary = 2 * TestUtils.EmployeeBaseSalary +
                                                TestUtils.EmployeeBaseSalary / 100 * 5 * TestUtils.EmployeeIncrementForYear +
                                                TestUtils.EmployeeBaseSalary / 100 * TestUtils.EmployeeIncrementForMaxYears;

            double salary = manager.CountSalary(SystemTime.Now);

            Assert.AreEqual(TestUtils.ManagerBaseSalary + (TestUtils.ManagerBaseSalary / 100 * TestUtils.ManagerIncrementForYear * 5) +
                            ((subordinateEmployeesSalary + subordinateManagerSalary) * 0.5 / 100),
                actual: salary,
                delta: 1);
        }
    }
}