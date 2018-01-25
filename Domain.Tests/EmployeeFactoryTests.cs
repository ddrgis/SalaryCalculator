using System;
using System.Diagnostics;
using Domain.Core.Entities;
using Domain.Core.Factories;
using Domain.Core.Interfaces;
using Domain.Core.Services;
using NUnit.Framework;

namespace Domain.Tests
{
    [TestFixture]
    public class EmployeeFactoryTests
    {
        [Test]
        public void CreateEmployee_WithWrongType_ReturnNull()
        {
            IEmployee employee = EmployeeFactory.CreateEmployee("WrongType", 2000, new DateTime(2000, 1,1 ));

            Assert.IsNull(employee);
        }

        [Test]
        public void CreateEmployee_WithoutConstructorArguments_ReturnNull()
        {
            IEmployee employee = EmployeeFactory.CreateEmployee("Manager");

            Assert.IsNull(employee);
        }

        [Test]
        public void CreateEmployee_InputEmployeeType_ReturnEmployeeInstance()
        {
            IEmployee employee = EmployeeFactory.CreateEmployee("Employee", 2000, SystemTime.Now, null, null);
            Debug.WriteLine(employee, "Tests");

            Assert.IsInstanceOf<Employee>(employee);
        }

        [Test]
        public void CreateEmployee_InputManagerType_ReturnManagerInstance()
        {
            IEmployee employee = EmployeeFactory.CreateEmployee("Manager", 3000, SystemTime.Now, 3, 50, 1, null);

            Assert.IsTrue(employee is Manager);
        }

        [Test]
        public void CreateEmployee_InputSalesmanType_ReturnSalesmanInstance()
        {
            IEmployee employee = EmployeeFactory.CreateEmployee("Salesman", 4000, SystemTime.Now, 2, 40, 2, null);

            Assert.IsInstanceOf(typeof(Salesman), employee);
        }
    }
}