using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Data.SQLite;
using System.Data.SQLite.Linq;
using Database;
using Domain.Core.Entities;
using Domain.Core.Interfaces;

namespace ConsoleSalaryCalculator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IEmployeeRepository repo = new EmployeeRepository();
            var employees = repo.List();

            foreach (IEmployee employee in employees)
            {
                Console.WriteLine($"{employee.GetType()} {employee.BaseSalary} {employee.DateOfEmployment}");
            }

            Console.ReadKey();
        }
    }
}