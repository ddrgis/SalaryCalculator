using Domain.Core;
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
            var fakeEmployeeRepository = Substitute.For<IEmployeeRepository>();
            var fakeEmployeeService = Substitute.For<IEmployeeService>();
            fakeEmployeeService.BuildTree().Returns(CreateDefaultEmployee());
            ISalaryService service = new SalaryService(fakeEmployeeRepository, fakeEmployeeService);

            double total = service.GetTotalSalary(SystemTime.Now);

            Assert.AreEqual(1000, total);
        }

        [Test]
        public void GetTotalSalary_ForFewEmployees_ReturnsTotalSalary()
        {
            SystemTime.Set(new DateTime(2000, 01, 01));
            var stubEmployee = new List<IEmployee>
            {
                CreateDefaultEmployee(),
                CreateDefaultEmployee(lengthOfWork: 5),
                CreateDefaultEmployee()
            };
            var fakeEmployeeRepository = Substitute.For<IEmployeeRepository>();
            var fakeEmployeeService = Substitute.For<IEmployeeService>();
            fakeEmployeeRepository.List().Returns(stubEmployee);
            ISalaryService service = new SalaryService(fakeEmployeeRepository, fakeEmployeeService);

            double total = service.GetTotalSalary(SystemTime.Now);

            //todo: Добавить возможность расчета суммарной зарплаты для изолированных деревьев (когда нет одного общего босса)
            Assert.AreEqual(3150, total);
        }

        [Test]
        public void GetTotalSalary_ForFullTreelikeStructure_ReturnsTotalSalary()
        {
            SystemTime.Set(new DateTime(2000, 01, 01));
            IEmployee treeOfEmployees = CreateFullTreeOfEmployees();
            var fakeEmployeeRepository = Substitute.For<IEmployeeRepository>();
            var fakeEmployeeService = Substitute.For<IEmployeeService>();
            fakeEmployeeService.BuildTree().Returns(treeOfEmployees);
            ISalaryService service = new SalaryService(fakeEmployeeRepository, fakeEmployeeService);

            double total = service.GetTotalSalary(SystemTime.Now);

            Assert.AreEqual(40_895.37, total, 1);
        }

        [Test]
        public void GetTotalSalary_ForBottomPartOfTree_ReturnsTotalSalary()
        {
            SystemTime.Set(new DateTime(2000, 01, 01));
            var fakeEmployeeRepository = Substitute.For<IEmployeeRepository>();
            var fakeEmployeeService = Substitute.For<IEmployeeService>();
            fakeEmployeeService.BuildTree().Returns(CreateSmallTreeOfEmployees());
            ISalaryService service = new SalaryService(fakeEmployeeRepository, fakeEmployeeService);

            double total = service.GetTotalSalary(SystemTime.Now);

            Assert.AreEqual(12155.42, total, 1);
        }

        [Test]
        public void GetTotalSalary_ForFullTree_ReturnsTotalSalaryWithSubordinatesPercents()
        {
            SystemTime.Set(new DateTime(2000, 01, 01));
            IEmployee stubTreeOfEmployees = CreateFullTreeOfEmployees();
            var fakeEmployeeRepository = Substitute.For<IEmployeeRepository>();
            var fakeEmployeeService = Substitute.For<IEmployeeService>();
            fakeEmployeeService.BuildTree().Returns(stubTreeOfEmployees);
            ISalaryService service = new SalaryService(fakeEmployeeRepository, fakeEmployeeService);

            double total = service.GetTotalSalary(SystemTime.Now);

            Assert.AreEqual(40_895.37, total, 1);
        }

        [Test]
        public void GetTotalSalary_ForMediumTree_ReturnsTotalSalaryWithSubordinatesPercents()
        {
            SystemTime.Set(new DateTime(2000, 01, 01));
            IEmployee stubTreeOfEmployees = CreateMediumTreeOfEmployees();
            var fakeEmployeeRepository = Substitute.For<IEmployeeRepository>();
            var fakeEmployeeService = Substitute.For<IEmployeeService>();
            fakeEmployeeService.BuildTree().Returns(stubTreeOfEmployees);
            ISalaryService service = new SalaryService(fakeEmployeeRepository, fakeEmployeeService);

            double total = service.GetTotalSalary(SystemTime.Now);

            Assert.AreEqual(19_603.49, total, 1);
        }
    }
}