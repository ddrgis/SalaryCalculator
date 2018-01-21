using Domain.Core;
using Domain.Core.Entities;
using Domain.Core.Interfaces;
using Domain.Core.Services;

namespace Domain.Tests
{
    public static class TestUtils
    {
        internal static double EmployeeBaseSalary = 1000;
        internal static double EmployeeYearIncrement = 3;
        internal static double EmployeeMaxYearIncrement = 30;

        internal static double ManagerBaseSalary = 2000;
        internal static double ManagerYearIncrement = 5;
        internal static double ManagerMaxYearIncrement = 40;
        internal static double ManagerSubordinatesIncrement = 0.5;

        internal static double SalesmanBaseSalary = 3000;
        internal static double SalesmanYearIncrement = 1;
        internal static double SalesmanMaxYearIncrement = 35;
        internal static double SalesmanSubordinatesIncrement = 0.3;

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