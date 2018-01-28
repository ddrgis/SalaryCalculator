using Domain.Core.Interfaces;
using Domain.Core.Services;
using NUnit.Framework;
using System;

namespace Domain.Tests.EntitiesTests
{
    [TestFixture]
    public class PersonTests
    {
        [TearDown]
        public void ResetAfterEachTest()
        {
            SystemTime.Reset();
        }

        [Test]
        public void CountSalary_NewEmployee_ReturnBaseSalary()
        {
            SystemTime.Set(new DateTime(2000, 1, 1));
            IEmployee employee = TestUtils.CreateDefaultEmployee();

            double salary = employee.CountSalary(SystemTime.Now);

            Assert.AreEqual(TestUtils.EmployeeBaseSalary, salary);
        }

        [Test]
        public void CountSalary_WithoutPayDateInput_ReturnBaseSalary()
        {
            SystemTime.Set(new DateTime(2000, 1, 1));
            IEmployee employee = TestUtils.CreateDefaultEmployee();

            double salary = employee.CountSalary();

            Assert.AreEqual(TestUtils.EmployeeBaseSalary, salary);
        }

        [Test]
        public void CountSalary_OneYearEmployee_ReturnBaseSalaryWithOneYearIncrement()
        {
            SystemTime.Set(new DateTime(2000, 1, 1));
            IEmployee employee = TestUtils.CreateDefaultEmployee(lengthOfWork: 1);
            double expectedSalary = TestUtils.EmployeeBaseSalary + (TestUtils.EmployeeBaseSalary / 100 * TestUtils.EmployeeIncrementForYear * 1);

            double actualSalary = employee.CountSalary(SystemTime.Now);

            Assert.AreEqual(expectedSalary, actualSalary);
        }

        [Test]
        public void CountSalary_FiftyYearEmployee_ReturnBaseSalaryWithMaxYearIncrement()
        {
            SystemTime.Set(new DateTime(2000, 1, 1));
            IEmployee employee = TestUtils.CreateDefaultEmployee(lengthOfWork: 50);
            double expectedSalary = TestUtils.EmployeeBaseSalary + (TestUtils.EmployeeBaseSalary / 100 * TestUtils.EmployeeIncrementForMaxYears);

            double actualSalary = employee.CountSalary(SystemTime.Now);

            Assert.AreEqual(expectedSalary, actualSalary);
        }
    }
}