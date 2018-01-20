using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entities;
using Domain.Core.Interfaces;
using Domain.Core.Services;
using NUnit.Framework;


namespace Domain.Tests
{
    [TestFixture]
    public class PersonTests
    {
        [Test]
        public void CountSalary_NewEmployee_ReturnBaseSalary()
        {
            SystemTime.Set(new DateTime(2000, 1, 1));
            var dateOfEmployment = SystemTime.Now;
            const double baseSalary = 1000;
            IEmployee employee = new Employee(baseSalary, dateOfEmployment);

            double salary = employee.CountSalary(SystemTime.Now);

            Assert.AreEqual(baseSalary, salary);
        }

        [Test]
        public void CountSalary_OneYearEmployee_ReturnBaseSalaryWithYearIncrement()
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

        [TearDown]
        public void ResetAfterEachTest()
        {
            SystemTime.Reset();
        }
    }
}
