﻿using Dapper;
using Domain.Core.Factories;
using Domain.Core.Interfaces;
using Domain.Interfaces;
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
        private IDbConnection CreateConnection()
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
            const string sql = "INSERT INTO Employees ( SuperiorId, FirstName, LastName, BaseSalary, DateOfEmployment, PercentageIncrementForYear, MaxPercentageIncrementForYear, PercentageIncrementFromSubordinates, Discriminator ) VALUES (@SuperiorId, @FirstName, @LastName, @BaseSalary, @DateOfEmployment, @PercentageIncrementForYear, @MaxPercentageIncrementForYear, @PercentageIncrementFromSubordinates, @Discriminator )";
            try
            {
                using (IDbConnection connection = CreateConnection())
                {
                    connection.Open();
                    connection.Execute(sql, new
                    {
                        entity.SuperiorId,
                        entity.FirstName,
                        entity.LastName,
                        entity.BaseSalary,
                        entity.DateOfEmployment,
                        entity.PercentageIncrementForYear,
                        entity.MaxPercentageIncrementForYear,
                        entity.PercentageIncrementFromSubordinates,
                        Discriminator = entity.GetType().Name
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Can not insert employee ({entity}) into table", ex);
            }
        }


        private List<IEmployee> Subordinates(int? id)
        {
            const string sql = "SELECT * FROM Employees WHERE SuperiorId = @Id";

            var subordinates = new List<IEmployee>();
            using (IDataReader reader = CreateConnection().ExecuteReader(sql, new {id}))
            {
                while (reader.Read())
                {
                    IEmployee employee = EmployeeFactory.Create(reader);
                    subordinates.Add(employee);
                }
            }

            return subordinates;
        }

        public void Delete(IEmployee entity)
        {
            const string sql = "DELETE FROM Employees WHERE Id = @Id";
            
            try
            {
                using (IDbConnection connection = CreateConnection())
                {
                    int? newSuperiorId = entity.SuperiorId;
                    connection.Open();
                    List<IEmployee> subordinates = Subordinates(entity.Id);
                    using (IDbTransaction transaction = connection.BeginTransaction())
                    {
                        connection.Execute(sql, new
                        {
                            entity.Id
                        });

                        foreach (IEmployee subordinate in subordinates)
                        {
                            subordinate.SuperiorId = newSuperiorId;
                            Edit(subordinate, connection);
                        }

                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Can not delete employee ({entity})", ex);
            }
        }

        //todo: refactor this. (connection argument)
        public void Edit(IEmployee entity, IDbConnection connection = null)
        {
            const string sql = "UPDATE Employees SET SuperiorId = @SuperiorId, FirstName = @FirstName, LastName = @LastName, BaseSalary = @BaseSalary, DateOfEmployment = @DateOfEmployment, PercentageIncrementForYear = @PercentageIncrementForYear, MaxPercentageIncrementForYear = @MaxPercentageIncrementForYear, PercentageIncrementFromSubordinates = @PercentageIncrementFromSubordinates, Discriminator = @Discriminator WHERE Id = @Id";
            string date = entity.DateOfEmployment.ToString("dd.MM.yyyy");
            try
            {
                var sqlParams = new
                {
                    entity.Id,
                    entity.SuperiorId,
                    entity.FirstName,
                    entity.LastName,
                    entity.BaseSalary,
                    DateOfEmployment = date,
                    entity.PercentageIncrementForYear,
                    entity.MaxPercentageIncrementForYear,
                    entity.PercentageIncrementFromSubordinates,
                    Discriminator = entity.GetType().Name
                };
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Execute(sql, sqlParams);
                    return;
                }

                using (connection = CreateConnection())
                {
                    connection.Open();
                    connection.Execute(sql, sqlParams);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Can not update employee ({entity})", ex);
            }
        }
    }
}