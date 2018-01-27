using Dapper;
using Domain.Core.Entities;
using Domain.Core.Factories;
using Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;

namespace Infrastructure.Database
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionStringName;

        //todo: вынести создание строк подлючений
        public IDbConnection CreateConnection()
        {
            return new SQLiteConnection(ConfigurationManager.ConnectionStrings[_connectionStringName].ConnectionString);
        }

        public EmployeeRepository(string connectionStringName = "EmployeeDB")
        {
            _connectionStringName = connectionStringName;
        }

        public List<IEmployee> List()
        {
            const string sql = "SELECT * FROM Employees";

            var employees = new List<IEmployee>();
            using (IDataReader reader = CreateConnection().ExecuteReader(sql))
            {
                while (reader.Read())
                {
                    //todo: exception handling when employee type is wrong and possible exception from DateTime.Parse
                    string employeeType = reader.GetString(reader.GetOrdinal("Discriminator"));
                    employees.Add(EmployeeFactory.Create(employeeType,
                        reader.GetDouble(reader.GetOrdinal("BaseSalary")), DateTime.Parse(reader.GetString(reader.GetOrdinal("DateOfEmployment")))));
                }
            }

            return employees;
        }

        public IEmployee GetById(int id)
        {
            const string sql = "SELECT * FROM Employees WHERE Id = @Id";

            using (IDbConnection connection = CreateConnection())
            {
                return EmployeeFactory.Create(connection.QueryFirstOrDefault(sql, new { id }));
            }
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