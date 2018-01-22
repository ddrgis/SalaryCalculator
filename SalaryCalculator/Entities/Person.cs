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
        public double YearSalaryIncrement { get; set; }
        public double MaxYearIncrement { get; set; }
        public double SubordinateIncrement { get; set; }
        public List<IEmployee> Subordinates { get; set; }

        protected Person(double baseSalary, DateTime dateOfEmployment, double yearSalaryIncrement, double maxYearIncrement,
                         double subordinatesIncrement = 0, List<IEmployee> subordinates = null)
        {
            BaseSalary = baseSalary;
            DateOfEmployment = dateOfEmployment;
            YearSalaryIncrement = yearSalaryIncrement;
            MaxYearIncrement = maxYearIncrement;
            SubordinateIncrement = subordinatesIncrement;
            Subordinates = subordinates;
        }

        public abstract double CountSalary(DateTime? payDate);

        protected double CountSalaryIncrement(DateTime? payDate)
        {
            if (payDate == null)
            {
                payDate = SystemTime.Now;
            }

            return GetYearIncrement(payDate) + GetSubordinatesIncrement(payDate);
        }

        public virtual double GetSubordinatesIncrement(DateTime? upToDate)
        {
            if (Subordinates == null)
            {
                return 0;
            }

            return Subordinates.Aggregate(0.0, (acc, emp) => acc + emp.CountSalary(upToDate)) / 100 * SubordinateIncrement;
        }

        private double GetYearIncrement(DateTime? upToDate)
        {
            double lengthOfWork = GetLengthOfWork(upToDate);
            double increment = lengthOfWork * YearSalaryIncrement;
            return increment > MaxYearIncrement
                ? (BaseSalary / 100) * MaxYearIncrement
                : (BaseSalary / 100) * increment;
        }

        private double GetLengthOfWork(DateTime? upToDate)
        {
            return SystemTime.PassedYears(DateOfEmployment, upToDate ?? SystemTime.Now);
        }
    }
}