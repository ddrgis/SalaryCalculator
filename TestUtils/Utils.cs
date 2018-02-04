using Domain.Core;
using Domain.Core.Entities;
using Domain.Core.Interfaces;
using System;
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

        public static List<IEmployee> CreateFullListOfEmployees()
        {
            return new List<IEmployee>()
            {
#pragma warning disable 618
                new Manager {Id = 1, SuperiorId = null, FirstName = "Alan", LastName = "Kay", BaseSalary = 8000, DateOfEmployment = new DateTime(1970,  10,  20), PercentageIncrementForYear = 3, MaxPercentageIncrementForYear = 30, PercentageIncrementFromSubordinates = 0.6},
                new Employee {Id = 2, SuperiorId = 1, FirstName = "Grady", LastName = "Booch", BaseSalary = 3000, DateOfEmployment = new DateTime(1980,  10,  20), PercentageIncrementForYear = 3, MaxPercentageIncrementForYear = 30, PercentageIncrementFromSubordinates = 0},
                new Employee {Id = 3, SuperiorId = 1, FirstName = "Anders", LastName = "Hejlsberg", BaseSalary = 3000, DateOfEmployment = new DateTime(1990,  10,  20), PercentageIncrementForYear = 3, MaxPercentageIncrementForYear = 30, PercentageIncrementFromSubordinates = 0},
                new Employee {Id = 4, SuperiorId = 1, FirstName = "Jeffrey", LastName = "Richter", BaseSalary = 3000, DateOfEmployment = new DateTime(2000,  01,  01), PercentageIncrementForYear = 3, MaxPercentageIncrementForYear = 30, PercentageIncrementFromSubordinates = 0},
                new Manager {Id = 5, SuperiorId = 1, FirstName = "Tim", LastName = "Berners-Lee", BaseSalary = 4000, DateOfEmployment = new DateTime(1995,  10,  20), PercentageIncrementForYear = 5, MaxPercentageIncrementForYear = 40, PercentageIncrementFromSubordinates = 0.5},
                new Employee {Id = 6, SuperiorId = 5, FirstName = "Ada", LastName = "Lovelace", BaseSalary = 2000, DateOfEmployment = new DateTime(1975,  10,  10), PercentageIncrementForYear = 3, MaxPercentageIncrementForYear = 30, PercentageIncrementFromSubordinates = 0},
                new Salesman {Id = 7, SuperiorId = 5, FirstName = "Larry", LastName = "Page", BaseSalary = 7000, DateOfEmployment = new DateTime(2000,  01,  01), PercentageIncrementForYear = 3, MaxPercentageIncrementForYear = 30, PercentageIncrementFromSubordinates = 0.3},
                new Employee {Id = 8, SuperiorId = 7, FirstName = "Steve", LastName = "Wozniak", BaseSalary = 2000, DateOfEmployment = new DateTime(1990,  10,  16), PercentageIncrementForYear = 4, MaxPercentageIncrementForYear = 30, PercentageIncrementFromSubordinates = 0},
                new Employee {Id = 9, SuperiorId = 7, FirstName = "Bill", LastName = "Gates", BaseSalary = 2000, DateOfEmployment = new DateTime(1990,  10,  16), PercentageIncrementForYear = 3, MaxPercentageIncrementForYear = 30, PercentageIncrementFromSubordinates = 0},
#pragma warning restore 618
            };
        }

        public static List<IEmployee> CreateSmallListOfEmployees()
        {
            return new List<IEmployee>()
            {
#pragma warning disable 618
                new Salesman {Id = 7, SuperiorId = 5, FirstName = "Larry", LastName = "Page", BaseSalary = 7000, DateOfEmployment = new DateTime(2000,  01,  01), PercentageIncrementForYear = 3, MaxPercentageIncrementForYear = 30, PercentageIncrementFromSubordinates = 0.3},
                new Employee {Id = 8, SuperiorId = 7, FirstName = "Steve", LastName = "Wozniak", BaseSalary = 2000, DateOfEmployment = new DateTime(1990,  10,  16), PercentageIncrementForYear = 4, MaxPercentageIncrementForYear = 30, PercentageIncrementFromSubordinates = 0},
                new Employee {Id = 9, SuperiorId = 7, FirstName = "Bill", LastName = "Gates", BaseSalary = 2000, DateOfEmployment = new DateTime(1990,  10,  16), PercentageIncrementForYear = 3, MaxPercentageIncrementForYear = 30, PercentageIncrementFromSubordinates = 0},
#pragma warning restore 618
            };
        }
    }
}

// new Manager {Id = 1, SuperiorId = null, FirstName = "Alan", LastName = "Kay", BaseSalary = 8000,
//                    DateOfEmployment = new DateTime(1970,  10,  20), PercentageIncrementForYear = 3, MaxPercentageIncrementForYear = 30,
//                    PercentageIncrementFromSubordinates = 0.6, Subordinates = new List<IEmployee>()
//                {
//                    new Employee {Id = 2, SuperiorId = 1, FirstName = "Grady", LastName = "Booch", BaseSalary = 3000, DateOfEmployment = new DateTime(1980,  10,  20), PercentageIncrementForYear = 3, MaxPercentageIncrementForYear = 30, PercentageIncrementFromSubordinates = 0},
//                    new Employee {Id = 3, SuperiorId = 1, FirstName = "Anders", LastName = "Hejlsberg", BaseSalary = 3000, DateOfEmployment = new DateTime(1990,  10,  20), PercentageIncrementForYear = 3, MaxPercentageIncrementForYear = 30, PercentageIncrementFromSubordinates = 0},
//                    new Employee {Id = 4, SuperiorId = 1, FirstName = "Jeffrey", LastName = "Richter", BaseSalary = 3000, DateOfEmployment = new DateTime(2000,  10,  20), PercentageIncrementForYear = 3, MaxPercentageIncrementForYear = 30, PercentageIncrementFromSubordinates = 0},
//                    new Manager {Id = 5, SuperiorId = 1, FirstName = "Tim", LastName = "Berners-Lee", BaseSalary = 4000, DateOfEmployment = new DateTime(1995,  10,  20), PercentageIncrementForYear = 5, MaxPercentageIncrementForYear = 40, PercentageIncrementFromSubordinates = 0.5, Subordinates = new List<IEmployee>()
//                    {
//                        new Employee {Id = 6, SuperiorId = 5, FirstName = "Ada", LastName = "Lovelace", BaseSalary = 2000, DateOfEmployment = new DateTime(1975,  10,  10), PercentageIncrementForYear = 3, MaxPercentageIncrementForYear = 30, PercentageIncrementFromSubordinates = 0},
//                        new Salesman {Id = 7, SuperiorId = 5, FirstName = "Larry", LastName = "Page", BaseSalary = 7000, DateOfEmployment = new DateTime(2000,  10,  10), PercentageIncrementForYear = 3, MaxPercentageIncrementForYear = 30, PercentageIncrementFromSubordinates = 0.3, Subordinates = new List<IEmployee>()
//                        {
//                            new Employee {Id = 8, SuperiorId = 7, FirstName = "Steve", LastName = "Wozniak", BaseSalary = 2000, DateOfEmployment = new DateTime(1990,  10,  16), PercentageIncrementForYear = 4, MaxPercentageIncrementForYear = 30, PercentageIncrementFromSubordinates = 0},
//                            new Employee {Id = 9, SuperiorId = 7, FirstName = "Bill", LastName = "Gates", BaseSalary = 2000, DateOfEmployment = new DateTime(1990,  10,  16), PercentageIncrementForYear = 3, MaxPercentageIncrementForYear = 30, PercentageIncrementFromSubordinates = 0},
//                        }},
//                    }},
//                }},