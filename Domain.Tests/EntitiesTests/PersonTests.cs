using NUnit.Framework;
using System;
using Domain.Core;
using Domain.Core.Entities;
using Domain.Core.Factories;
using Domain.Core.Interfaces;
using NSubstitute;

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

        [Test]
        public void ToString_ByDefault_ReturnsFullInfo()
        {
            var stubEmployee = new Employee(2000, new DateTime(2000, 11, 10), 3, 30)
            {
                Id = 55,
                SuperiorId = null,
                FirstName = "Nikita",
                LastName = "Sementcov"
            };

            string actual = stubEmployee.ToString();

            Assert.AreEqual("Employee: Nikita Sementcov, Id = 55, SuperiorId = null, BaseSalary - 2000, DateOfEmployement - 10.11.2000," +
                            " Percentage Increment For Year - 3, Max Percentage Increment For Years - 30," +
                            " Percentage Increment From Subordinates - 0", actual);
        }
    }
}