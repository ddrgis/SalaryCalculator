using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core;
using Domain.Core.Entities;
using Domain.Core.Interfaces;
using Domain.Core.Services;

namespace Domain.Tests
{
    [TestFixture]
    public class ManagerTests
    {
        [Test]
        public void CountSalary_NewManagerWithFewSubordinates_ReturnSalaryWithIncrement()
        {
            SystemTime.Set(new DateTime(2000, 1, 1));
            IEmployee manager = new Manager(
                baseSalary: 2000,
                dateOfEmployment: SystemTime.Now,
                subordinatesIncrement: 0.5,
                subordinates: new List<IEmployee>
                {
                    new Employee(baseSalary: 1000, dateOfEmployment: SystemTime.Now),
                    new Employee(baseSalary: 1000, dateOfEmployment: SystemTime.Now)
                }
            );

            double salary = manager.CountSalary(SystemTime.Now);

            Assert.AreEqual(2010, salary);
        }
    }
}