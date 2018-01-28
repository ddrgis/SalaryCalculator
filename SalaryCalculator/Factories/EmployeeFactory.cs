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
        public static IEmployee Create(string employeeType, string firstName, string lastName, double baseSalary, 
            DateTime dateOfEmployment, List<IEmployee> subordinates = null, int? superiorId = null)
        {
            //todo: factory without switch-case
            //todo: add remaining (optionals) parameters
            switch (employeeType)
            {
                case "Employee":
                    return new Employee(firstName, lastName, baseSalary, dateOfEmployment, superiorId: superiorId);

                case "Manager":
                    return new Manager(firstName, lastName, baseSalary, dateOfEmployment, subordinates: subordinates, superiorId: superiorId);

                case "Salesman":
                    return new Salesman(firstName, lastName, baseSalary, dateOfEmployment, subordinates: subordinates, superiorId: superiorId);

                default:
                    return null;
            }
        }

        //todo: refactor this
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