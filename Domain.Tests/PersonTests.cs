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
        [Test]
        public void CountSalary_NewEmployee_ReturnBaseSalary()
        {
            SystemTime.Set(new DateTime(2000, 1, 1));
            const double baseSalary = 1000;
            IEmployee employee = new Employee(baseSalary: 1000, dateOfEmployment: SystemTime.Now);

            double salary = employee.CountSalary(SystemTime.Now);

            Assert.AreEqual(baseSalary, salary);
        }

        [Test]
        public void CountSalary_OneYearEmployee_ReturnBaseSalaryWithOneYearIncrement()
        {
            SystemTime.Set(new DateTime(2000, 1, 1));
            const int lenghtOfWork = 1;
            var dateOfEmployment = SystemTime.Now.AddYears(-lenghtOfWork);
            const double baseSalary = 1000;
            IEmployee employee = new Employee(baseSalary, dateOfEmployment, 3);
            double expectedSalary = baseSalary + (baseSalary / 100 * employee.YearSalaryIncrement * lenghtOfWork);

            double actualSalary = employee.CountSalary(SystemTime.Now);

            Assert.AreEqual(expectedSalary, actualSalary);
        }

        [Test]
        public void CountSalary_FiftyYearEmployee_ReturnBaseSalaryWithMaxYearIncrement()
        {
            SystemTime.Set(new DateTime(2000, 1, 1));
            var dateOfEmployment = SystemTime.Now.AddYears(-50);
            const double baseSalary = 1000;
            const double maxYearIncrement = 30;
            IEmployee employee = new Employee(baseSalary, dateOfEmployment, maxYearIncrement: maxYearIncrement);
            const double expectedSalary = baseSalary + (baseSalary / 100 * maxYearIncrement);

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