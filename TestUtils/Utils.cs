using Domain.Core;
using Domain.Core.Entities;
using Domain.Core.Interfaces;
using System.Collections.Generic;

namespace TestUtils
{
    public static class Utils
    {
        public static double EmployeeBaseSalary = 1000;
        public static double EmployeeIncrementForYear = 3;
        public static double EmployeeIncrementForMaxYears = 30;

        public static double ManagerBaseSalary = 2000;
        public static double ManagerIncrementForYear = 5;
        public static double ManagerIncrementForMaxYears = 40;
        public static double ManagerIncrementFromSubordinates = 0.5;

        public static double SalesmanBaseSalary = 3000;
        public static double SalesmanIncrementForYear = 1;
        public static double SalesmanIncrementForMaxYears = 35;
        public static double SalesmanIncrementFromSubordinates = 0.3;

        public static IEmployee CreateDefaultEmployee(int lengthOfWork = 0, List<IEmployee> subordinates = null)
        {
            return new Employee("Nikita", "Sementcov", baseSalary: EmployeeBaseSalary, dateOfEmployment: SystemTime.Now.AddYears(-lengthOfWork));
        }

        public static IEmployee CreateDefaultManager(int lengthOfWork = 0, List<IEmployee> subordinates = null)
        {
            return new Manager("Nikita", "Sementcov", baseSalary: ManagerBaseSalary,
                dateOfEmployment: SystemTime.Now.AddYears(-lengthOfWork), subordinates: subordinates);
        }

        public static IEmployee CreateDefaultSalesman(int lengthOfWork = 0, List<IEmployee> subordinates = null)
        {
            return new Salesman("Nikita", "Sementcov", baseSalary: SalesmanBaseSalary,
                dateOfEmployment: SystemTime.Now.AddYears(-lengthOfWork), subordinates: subordinates);
        }
    }
}