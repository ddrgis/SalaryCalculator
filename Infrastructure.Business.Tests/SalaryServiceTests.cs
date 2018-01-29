﻿using Domain.Core;
using Domain.Core.Interfaces;
using Domain.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using static TestUtils.Utils;

namespace Infrastructure.Business.Tests
{
    [TestFixture]
    public class SalaryServiceTests
    {
        [TearDown]
        public void ResetAfterEachTest()
        {
            SystemTime.Reset();
        }

        [Test]
        public void GetTotalSalary_ForOneEmployee_ReturnsHisSalary()
        {
            SystemTime.Set(new DateTime(2000, 01, 01));
            var stubEmployee = new List<IEmployee>
            {
                CreateDefaultEmployee()
            };
            var fakeEmployeeRepository = Substitute.For<IEmployeeRepository>();
            fakeEmployeeRepository.List().Returns(stubEmployee);
            ISalaryService service = new SalaryService(fakeEmployeeRepository);

            double total = service.GetTotalSalary(SystemTime.Now);

            Assert.AreEqual(1000, total);
        }
    }
}