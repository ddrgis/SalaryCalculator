using Dapper;
using Domain.Core.Factories;
using Domain.Core.Interfaces;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;

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
                    IEmployee employee = EmployeeFactory.Create(reader);
                    employees.Add(employee);
                }
            }

            return employees;
        }

        public IEmployee GetById(int id)
        {
            const string sql = "SELECT * FROM Employees WHERE Id = @Id";
            try
            {
                using (IDataReader reader = CreateConnection().ExecuteReader(sql, new { id }))
                {
                    reader.Read();
                    return EmployeeFactory.Create(reader);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Can not read employee with Id = {id}", ex);
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