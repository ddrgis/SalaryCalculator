using Domain.Interfaces;
using Infrastructure.Database;
using System;
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

            Console.WriteLine();

            var emp2 = repo.GetById(2);
            Console.WriteLine($"{emp2.GetType()} {emp2.BaseSalary} {emp2.DateOfEmployment}");

            var emp3 = repo.GetById(3);
            Console.WriteLine($"{emp3.GetType()} {emp3.BaseSalary} {emp3.DateOfEmployment}");

            Console.ReadKey();
        }
    }
}