using Domain.Core;
using Domain.Core.Entities;
using Domain.Core.Interfaces;
using NUnit.Framework;
using System;
using static TestUtils.Utils;

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
            double expectedSalary = EmployeeBaseSalary + (EmployeeBaseSalary / 100 * EmployeeIncrementForYear * 1);

            double actualSalary = employee.CountSalary(SystemTime.Now);

            Assert.AreEqual(expectedSalary, actualSalary);
        }

        [Test]
        public void CountSalary_FiftyYearEmployee_ReturnBaseSalaryWithMaxYearIncrement()
        {
            SystemTime.Set(new DateTime(2000, 1, 1));
            IEmployee employee = CreateDefaultEmployee(lengthOfWork: 50);
            double expectedSalary = EmployeeBaseSalary + (EmployeeBaseSalary / 100 * EmployeeIncrementForMaxYears);

            double actualSalary = employee.CountSalary(SystemTime.Now);

            Assert.AreEqual(expectedSalary, actualSalary);
        }

        [Test]
        public void ToString_ByDefault_ReturnsFullInfo()
        {
            var stubEmployee = new Employee("Nikita", "Sementcov", 2000, new DateTime(2000, 11, 10), 3, 30);

            string actual = stubEmployee.ToString();

            Assert.AreEqual("Employee: Nikita Sementcov, Id = 0, SuperiorId = null, BaseSalary - 2000, DateOfEmployement - 10.11.2000," +
                            " Percentage Increment For Year - 3, Max Percentage Increment For Years - 30," +
                            " Percentage Increment From Subordinates - 0", actual);
        }
    }
}