using Domain.Core.Entities;
using Domain.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace Domain.Core.Factories
{
    public static class EmployeeFactory
    {
        public static IEmployee CreateEmployee(string employeeType, double baseSalary, DateTime dateOfEmployment, List<IEmployee> subordinates = null)
        {
            //todo: factory without switch-case
            //todo: add remaining (optionals) parameters
            switch (employeeType)
            {
                case "Employee":
                    return new Employee(baseSalary, dateOfEmployment);

                case "Manager":
                    return new Manager(baseSalary, dateOfEmployment, subordinates: subordinates);

                case "Salesman":
                    return new Salesman(baseSalary, dateOfEmployment, subordinates: subordinates);

                default:
                    return null;
            }
        }
    }
}