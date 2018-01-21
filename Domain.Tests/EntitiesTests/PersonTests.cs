using Domain.Core.Interfaces;
using Domain.Core.Services;
using NUnit.Framework;
using System;
using static Domain.Tests.EntitiesTests.TestUtils;

namespace Domain.Tests
{
    [TestFixture]
    public class PersonTests
    {
        [Test]
        public void CountSalary_NewEmployee_ReturnBaseSalary()
        {
            SystemTime.Set(new DateTime(2000, 1, 1));
            IEmployee employee = CreateDefaultEmployee();

            double salary = employee.CountSalary(SystemTime.Now);

            Assert.AreEqual(EmployeeBaseSalary, salary);
        }

        [Test]
        public void CountSalary_WithoutPayDateInput_ReturnBaseSalary()
        {
            SystemTime.Set(new DateTime(2000, 1, 1));
            IEmployee employee = CreateDefaultEmployee();

            double salary = employee.CountSalary();

            Assert.AreEqual(EmployeeBaseSalary, salary);
        }

        [Test]
        public void CountSalary_OneYearEmployee_ReturnBaseSalaryWithOneYearIncrement()
        {
            SystemTime.Set(new DateTime(2000, 1, 1));
            IEmployee employee = CreateDefaultEmployee(lengthOfWork: 1);
            double expectedSalary = EmployeeBaseSalary + (EmployeeBaseSalary / 100 * EmployeeYearIncrement * 1);

            double actualSalary = employee.CountSalary(SystemTime.Now);

            Assert.AreEqual(expectedSalary, actualSalary);
        }

        [Test]
        public void CountSalary_FiftyYearEmployee_ReturnBaseSalaryWithMaxYearIncrement()
        {
            SystemTime.Set(new DateTime(2000, 1, 1));
            IEmployee employee = CreateDefaultEmployee(lengthOfWork: 50);
            double expectedSalary = EmployeeBaseSalary + (EmployeeBaseSalary / 100 * EmployeeMaxYearIncrement);

            double actualSalary = employee.CountSalary(SystemTime.Now);

            Assert.AreEqual(expectedSalary, actualSalary);
        }

        [TearDown]
        public void ResetAfterEachTest()
        {
            SystemTime.Reset();
        }
    }
}