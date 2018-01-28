using Domain.Core.Entities;
using Domain.Core.Factories;
using NUnit.Framework;
using System;
using Domain.Core;
using Domain.Core.Interfaces;

namespace Domain.Tests
{
    [TestFixture]
    public class EmployeeFactoryTests
    {
        [Test]
        public void CreateEmployee_WithWrongType_ReturnNull()
        {
            IEmployee employee = EmployeeFactory.Create("WrongType", 2000, new DateTime(2000, 1, 1));

            Assert.IsNull(employee);
        }

        [Test]
        public void CreateEmployee_InputEmployeeType_ReturnEmployeeInstance()
        {
            IEmployee employee = EmployeeFactory.Create("Employee", 2000, SystemTime.Now);

            Assert.IsInstanceOf<Employee>(employee);
        }

        [Test]
        public void CreateEmployee_InputEmployeeType_ReturnEmployeeWithRightDefaultValue()
        {
            IEmployee employee = EmployeeFactory.Create("Employee", 2000, SystemTime.Now);

            Assert.AreEqual(3, employee.PercentageIncrementForYear);
        }

        [Test]
        public void CreateEmployee_InputManagerType_ReturnManagerInstance()
        {
            IEmployee employee = EmployeeFactory.Create("Manager", 3000, SystemTime.Now);

            Assert.IsTrue(employee is Manager);
        }

        [Test]
        public void CreateEmployee_InputSalesmanType_ReturnSalesmanInstance()
        {
            IEmployee employee = EmployeeFactory.Create("Salesman", 4000, SystemTime.Now);

            Assert.IsInstanceOf(typeof(Salesman), employee);
        }
    }
}