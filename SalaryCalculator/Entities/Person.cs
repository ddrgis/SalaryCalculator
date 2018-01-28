using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Core.Interfaces;

namespace Domain.Core.Entities
{
    public abstract class Person : IEmployee
    {
        public int Id { get; }
        public int? SuperiorId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public double BaseSalary { get; }
        public DateTime DateOfEmployment { get; }
        public double PercentageIncrementForYear { get; }
        public double MaxPercentageIncrementForYear { get; }
        public double PercentageIncrementFromSubordinates { get; }
        public List<IEmployee> Subordinates { get; }

        [Obsolete("Constructor for ORM")]
        protected Person()
        {
            
        }

        protected Person(string firstName, string lastName, double baseSalary, DateTime dateOfEmployment, double percentageIncrementForYear,
            double maxPercentageIncrementForYear, double percentagesIncrementFromSubordinates = 0, List<IEmployee> subordinates = null, int? superiorId = null)
        {
            FirstName = firstName;
            LastName = lastName;
            BaseSalary = baseSalary;
            DateOfEmployment = dateOfEmployment;
            PercentageIncrementForYear = percentageIncrementForYear;
            MaxPercentageIncrementForYear = maxPercentageIncrementForYear;
            PercentageIncrementFromSubordinates = percentagesIncrementFromSubordinates;
            Subordinates = subordinates;
            SuperiorId = superiorId;
        }

        public abstract double CountSalary(DateTime? payDate);

        protected double CountSalaryIncrement(DateTime? payDate)
        {
            if (payDate == null)
            {
                payDate = SystemTime.Now;
            }

            return GetIncrementForYears(payDate) + GetIncrementFromSubordinates(payDate);
        }

        public virtual double GetIncrementFromSubordinates(DateTime? upToDate)
        {
            if (Subordinates == null)
            {
                return 0;
            }

            return Subordinates.Aggregate(0.0, (acc, emp) => acc + emp.CountSalary(upToDate)) / 100 * PercentageIncrementFromSubordinates;
        }

        public override string ToString()
        {
            string superiorId = SuperiorId == null ? "null" : SuperiorId.ToString();
            return $"{GetType().Name}: {FirstName} {LastName}, Id = {Id}, SuperiorId = {superiorId}, BaseSalary - {BaseSalary}, " +
                   $"DateOfEmployement - {DateOfEmployment:dd.MM.yyyy}, Percentage Increment For Year - {PercentageIncrementForYear}, " +
                   $"Max Percentage Increment For Years - {MaxPercentageIncrementForYear}," +
                   $" Percentage Increment From Subordinates - {PercentageIncrementFromSubordinates}";
        }

        private double GetIncrementForYears(DateTime? upToDate)
        {
            double lengthOfWork = GetLengthOfWork(upToDate);
            double incrementForYears = lengthOfWork * PercentageIncrementForYear;
            return incrementForYears > MaxPercentageIncrementForYear
                ? (BaseSalary / 100) * MaxPercentageIncrementForYear
                : (BaseSalary / 100) * incrementForYears;
        }

        private double GetLengthOfWork(DateTime? upToDate)
        {
            return SystemTime.PassedYears(DateOfEmployment, upToDate ?? SystemTime.Now);
        }
    }
}