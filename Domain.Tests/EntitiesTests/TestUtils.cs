using Domain.Core;
using Domain.Core.Entities;
using Domain.Core.Interfaces;

namespace Domain.Tests.EntitiesTests
{
    public static class TestUtils
    {
        internal static double EmployeeBaseSalary = 1000;
        internal static double EmployeeIncrementForYear = 3;
        internal static double EmployeeIncrementForMaxYears = 30;

        internal static double ManagerBaseSalary = 2000;
        internal static double ManagerIncrementForYear = 5;
        internal static double ManagerIncrementForMaxYears = 40;
        internal static double ManagerIncrementFromSubordinates = 0.5;

        internal static double SalesmanBaseSalary = 3000;
        internal static double SalesmanIncrementForYear = 1;
        internal static double SalesmanIncrementForMaxYears = 35;
        internal static double SalesmanIncrementFromSubordinates = 0.3;

        internal static IEmployee CreateDefaultEmployee(int lengthOfWork = 0)
        {
            return new Employee(baseSalary: EmployeeBaseSalary, dateOfEmployment: SystemTime.Now.AddYears(-lengthOfWork));
        }

        internal static IEmployee CreateDefaultManager(int lengthOfWork = 0)
        {
            return new Manager(baseSalary: ManagerBaseSalary, dateOfEmployment: SystemTime.Now.AddYears(-lengthOfWork));
        }

        internal static IEmployee CreateDefaultSalesman(int lengthOfWork = 0)
        {
            return new Salesman(baseSalary: SalesmanBaseSalary, dateOfEmployment: SystemTime.Now.AddYears(-lengthOfWork));
        }
    }
}