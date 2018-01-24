using System;
using System.Collections.Generic;
using Domain.Core.Entities;
using Domain.Core.Interfaces;
using Domain.Core.Services;
using System.Configuration;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SQLite;
using System.Linq;
using Dapper;

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

        public IEmployee FindById(int id)
        {
            throw new NotImplementedException();
        }
    }
}