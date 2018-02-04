using Domain.Core.Interfaces;
using Domain.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using static TestUtils.Utils;

namespace Infrastructure.Business.Tests
{
    [TestFixture]
    public class EmployeeServiceTests
    {
        [Test]
        public void BuildTree_ByDefault_ReturnsFullTree()
        {
            var stubRepository = Substitute.For<IEmployeeRepository>();
            List<IEmployee> stubListOfEmployees = CreateFullListOfEmployees();
            stubRepository.List().Returns(stubListOfEmployees);
            var employeeService = new EmployeeService(stubRepository);

            IEmployee tree = employeeService.BuildTree();

            //todo: Перезагрузить методы Equals и GetHashCode для Person
            Assert.AreEqual(CreateFullTreeOfEmployees(), tree);
        }
    }
}