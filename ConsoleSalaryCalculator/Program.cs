using System;
using System.Configuration;
using System.Data.SQLite;

namespace ConsoleSalaryCalculator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["EmployeeDB"].ConnectionString;
            const string sqlExpression = "SELECT * FROM Employees";
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var query = new SQLiteCommand(sqlExpression, connection);
                SQLiteDataReader reader = query.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(reader.GetValue(0));
                    Console.WriteLine(reader.GetValue(1));
                    Console.WriteLine(reader.GetValue(2));
                    Console.WriteLine(reader.GetValue(3));
                    Console.WriteLine();
                }
            }

            Console.Read();
        }
    }
}