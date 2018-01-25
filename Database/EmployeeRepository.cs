using System;
using Dapper;
using Domain.Core.Factories;
using Domain.Core.Interfaces;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;

namespace Database
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbConnection _connectionString;

        public EmployeeRepository()
        {
            _connectionString = new SQLiteConnection(ConfigurationManager.ConnectionStrings["EmployeeDB"].ConnectionString);
        }

        public EmployeeRepository(IDbConnection connectionString)
        {
            _connectionString = connectionString;
        }

        public List<IEmployee> List()
        {
            const string sql = "SELECT * FROM Employees";

            var employees = new List<IEmployee>();
            using (IDataReader reader = _connectionString.ExecuteReader(sql))
            {
                while (reader.Read())
                {
                    //todo: exception handling when employee type is wrong and possible exception from DateTime.Parse
                    string employeeType = reader.GetString(reader.GetOrdinal("Discriminator"));
                    employees.Add(EmployeeFactory.CreateEmployee(employeeType, 
                        reader.GetDouble(reader.GetOrdinal("BaseSalary")), DateTime.Parse(reader.GetString(reader.GetOrdinal("DateOfEmployment")))));
                }
            }

            return employees;
        }

        public IEmployee GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(IEmployee entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(IEmployee entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(IEmployee entity)
        {
            throw new NotImplementedException();
        }
    }
}