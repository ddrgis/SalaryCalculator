using Domain.Interfaces;
using Infrastructure.Database;
using System;
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

            Console.WriteLine("List of all persons:");
            foreach (IEmployee employee in employees)
            {
                Console.WriteLine(employee.ToString());
            }

            Console.WriteLine();

            Console.WriteLine("GetById Test:");
            IEmployee emp = repo.GetById(2);
            Console.WriteLine(emp.ToString());

            emp.BaseSalary += 100;
            repo.Edit(emp);

            IEmployee toDelete = repo.GetById(7);
            repo.Delete(toDelete);

            Console.ReadLine();
        }
    }
}