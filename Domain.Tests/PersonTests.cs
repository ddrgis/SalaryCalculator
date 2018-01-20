using Domain.Core.Entities;
using Domain.Core.Interfaces;
using Domain.Core.Services;
using NUnit.Framework;
using System;

namespace Domain.Tests
{
    [TestFixture]
    public class PersonTests
    {
        internal const double EmployeeBaseSalary = 1000;
        internal const double EmployeeYearIncrement = 3;
        internal const double EmployeeMaxYearIncrement = 30;

        internal IEmployee CreateDefaultEmployee(DateTime? dateOfEmployement = null)
        {
            return new Employee(EmployeeBaseSalary, dateOfEmployement ?? SystemTime.Now);
        }

        [Test]
        public void CountSalary_NewEmployee_ReturnBaseSalary()
        {
            SystemTime.Set(new DateTime(2000, 1, 1));
            IEmployee employee = CreateDefaultEmployee();

            double salary = employee.CountSalary(SystemTime.Now);

            Assert.AreEqual(EmployeeBaseSalary, salary);
        }

        [Test]
        public void CountSalary_OneYearEmployee_ReturnBaseSalaryWithOneYearIncrement()
        {
            SystemTime.Set(new DateTime(2000, 1, 1));
            IEmployee employee = CreateDefaultEmployee(SystemTime.Now.AddYears(-1));
            const double expectedSalary = EmployeeBaseSalary + (EmployeeBaseSalary / 100 * EmployeeYearIncrement * 1);

            double actualSalary = employee.CountSalary(SystemTime.Now);

            Assert.AreEqual(expectedSalary, actualSalary);
        }

        [Test]
        public void CountSalary_FiftyYearEmployee_ReturnBaseSalaryWithMaxYearIncrement()
        {
            SystemTime.Set(new DateTime(2000, 1, 1));
            IEmployee employee = new Employee(EmployeeBaseSalary, SystemTime.Now.AddYears(-50));
            const double expectedSalary = EmployeeBaseSalary + (EmployeeBaseSalary / 100 * EmployeeMaxYearIncrement);

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