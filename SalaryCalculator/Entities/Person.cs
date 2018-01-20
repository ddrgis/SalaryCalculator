using Domain.Core.Interfaces;
using Domain.Core.Services;
using System;

namespace Domain.Core.Entities
{
    public abstract class Person : IEmployee
    {
        public double BaseSalary { get; set; }
        public DateTime DateOfEmployment { set; get; }
        public double YearSalaryIncrement { get; set; }
        public double MaxYearIncrement { get; set; }

        protected Person(double baseSalary, DateTime dateOfEmployment, double yearSalaryIncrement, double maxYearIncrement)
        {
            BaseSalary = baseSalary;
            DateOfEmployment = dateOfEmployment;
            YearSalaryIncrement = yearSalaryIncrement;
            MaxYearIncrement = maxYearIncrement;
        }

        public abstract double CountSalary(DateTime? payDate);

        protected double CountSalaryIncrement(DateTime? payDate)
        {
            if (payDate == null)
            {
                payDate = SystemTime.Now;
            }
            return (BaseSalary / 100) * GetYearIncrement(payDate);
        }

        private double GetYearIncrement(DateTime? upToDate = null)
        {
            double lengthOfWork = GetLengthOfWork(upToDate);
            double increment = lengthOfWork * YearSalaryIncrement;
            return increment > MaxYearIncrement ? MaxYearIncrement : increment;
        }

        private double GetLengthOfWork(DateTime? upToDate = null)
        {
            return SystemTime.PassedYears(DateOfEmployment, upToDate ?? SystemTime.Now);
        }
    }
}