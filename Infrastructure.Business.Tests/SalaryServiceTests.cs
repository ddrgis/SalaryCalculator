using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core;
using Domain.Core.Entities;
using Domain.Core.Interfaces;
using Domain.Interfaces;
using NSubstitute;

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
            var stubEmployee = new List<IEmployee>()
            {
                new Employee(2000, SystemTime.Now)
            };
            var fakeEmployeeRepository = Substitute.For<IEmployeeRepository>();
            fakeEmployeeRepository.List().Returns(stubEmployee);
            ISalaryService service = new SalaryService(fakeEmployeeRepository);

            var total = service.GetTotalSalary(SystemTime.Now);

            Assert.AreEqual(2000, total);
        }
    }
}
