using Domain.Core;
using Domain.Core.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using static TestUtils.Utils;

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
            IEmployee manager = CreateDefaultManager(subordinates: new List<IEmployee> {
                CreateDefaultEmployee(),
                CreateDefaultEmployee()
            });

            double salary = manager.CountSalary(SystemTime.Now);

            Assert.AreEqual(ManagerBaseSalary + 2 * EmployeeBaseSalary * ManagerIncrementFromSubordinates / 100, salary);
        }

        [Test]
        public void CountSalary_WithoutPayDateInput_ReturnIncrementedSalary()
        {
            SystemTime.Set(new DateTime(2000, 1, 1));
            IEmployee manager = CreateDefaultManager(subordinates: new List<IEmployee> {
                CreateDefaultEmployee(),
                CreateDefaultEmployee()
            });

            double salary = manager.CountSalary();

            Assert.AreEqual(ManagerBaseSalary + 2 * EmployeeBaseSalary * ManagerIncrementFromSubordinates / 100, salary);
        }

        [Test]
        public void CountSalary_NewManagerWithEmployeeSubordinates_ReturnIncrementedSalary()
        {
            SystemTime.Set(new DateTime(2000, 1, 1));
            IEmployee manager = CreateDefaultManager(subordinates: new List<IEmployee> {
                CreateDefaultEmployee(lengthOfWork: 5),
                CreateDefaultEmployee(lengthOfWork: 40)
            });
            double subordinatesIncrementForYears = EmployeeBaseSalary / 100 * EmployeeIncrementForYear * 5 +
                                                EmployeeBaseSalary / 100 * EmployeeIncrementForMaxYears;

            double salary = manager.CountSalary(SystemTime.Now);

            Assert.AreEqual(ManagerBaseSalary +
                            (2 * EmployeeBaseSalary + subordinatesIncrementForYears) * ManagerIncrementFromSubordinates / 100, salary);
        }

        [Test]
        public void CountSalary_OldManagerWithTreelikeSubordinates_ReturnIncrementedSalary()
        {
            SystemTime.Set(new DateTime(2000, 1, 1));
            var managerSubordinates = new List<IEmployee> {
                CreateDefaultEmployee(lengthOfWork: 5),
                CreateDefaultEmployee(lengthOfWork: 40)
            };
            IEmployee manager = CreateDefaultManager(lengthOfWork: 5, subordinates: managerSubordinates);

            var subordinatesOfSubordinatemanager = new List<IEmployee> {
                CreateDefaultEmployee(lengthOfWork: 2),
                CreateDefaultEmployee(lengthOfWork: 2)
            };
            IEmployee subordinateManager = CreateDefaultManager(lengthOfWork: 2, subordinates: subordinatesOfSubordinatemanager);
            manager.Subordinates.Add(subordinateManager);
            double subordinateManagerSalary = ManagerBaseSalary + ManagerBaseSalary / 100 * ManagerIncrementForYear * 2 +
                (2 * (EmployeeBaseSalary + EmployeeBaseSalary / 100 * 2 * EmployeeIncrementForYear) * ManagerIncrementFromSubordinates / 100);
            double subordinateEmployeesSalary = 2 * EmployeeBaseSalary +
                                                EmployeeBaseSalary / 100 * 5 * EmployeeIncrementForYear +
                                                EmployeeBaseSalary / 100 * EmployeeIncrementForMaxYears;

            //ACT:
            double salary = manager.CountSalary(SystemTime.Now);

            Assert.AreEqual(ManagerBaseSalary + (ManagerBaseSalary / 100 * ManagerIncrementForYear * 5) +
                            ((subordinateEmployeesSalary + subordinateManagerSalary) * 0.5 / 100),
                actual: salary,
                delta: 1);
        }
    }
}