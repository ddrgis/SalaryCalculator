using Dapper;
using Domain.Core.Entities;
using Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;

namespace Domain.Core.Factories
{
    public static class EmployeeFactory
    {
        public static IEmployee Create(string employeeType, double baseSalary, DateTime dateOfEmployment, List<IEmployee> subordinates = null)
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

        public static IEmployee Create(IDataReader reader)
        {
            var employeeParser = reader.GetRowParser<Employee>();
            var managerParser = reader.GetRowParser<Manager>();
            var salesmanParser = reader.GetRowParser<Salesman>();

            IEmployee employee;

            string employeeType = reader.GetString(reader.GetOrdinal("Discriminator"));
            switch (employeeType)
            {
                case "Employee":
                    employee = employeeParser(reader);
                    break;

                case "Manager":
                    employee = managerParser(reader);
                    break;

                case "Salesman":
                    employee = salesmanParser(reader);
                    break;

                default:
                    throw new Exception($"Can not create person with {employeeType} type");
            }

            return employee;
        }
    }
}