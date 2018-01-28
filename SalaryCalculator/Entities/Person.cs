﻿using Domain.Core.Interfaces;
using Domain.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Core.Entities
{
    public abstract class Person : IEmployee
    {
        public int Id { get; set; }
        public int? SuperiorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double BaseSalary { get; set; }
        public DateTime DateOfEmployment { set; get; }
        public double PercentageIncrementForYear { get; set; }
        public double MaxPercentageIncrementForYear { get; set; }
        public double PercentageIncrementFromSubordinates { get; set; }
        public List<IEmployee> Subordinates { get; set; }

        protected Person(double baseSalary, DateTime dateOfEmployment, double percentageIncrementForYear,
            double maxPercentageIncrementForYear, double percentagesIncrementFromSubordinates = 0, List<IEmployee> subordinates = null)
        {
            BaseSalary = baseSalary;
            DateOfEmployment = dateOfEmployment;
            PercentageIncrementForYear = percentageIncrementForYear;
            MaxPercentageIncrementForYear = maxPercentageIncrementForYear;
            PercentageIncrementFromSubordinates = percentagesIncrementFromSubordinates;
            Subordinates = subordinates;
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