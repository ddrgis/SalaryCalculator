using System;
using Domain.Core.Interfaces;
using Domain.Core.Services;

namespace Domain.Core.Entities
{
    public abstract class Person : IEmployee
    {
        public DateTime DateOfEmployment { get; set; }
        public double BaseSalary { get; set; }
        public double YearSalaryIncrement { get; set; }

        public abstract double CountSalary(DateTime? payDate);

        protected double CountSalaryIncrement(DateTime? payDate)
        {
            if (payDate == null)
            {
                payDate = SystemTime.Now;
            }

            return (BaseSalary / 100) * GetLengthOfWork(payDate) * YearSalaryIncrement;
        }

        private double GetLengthOfWork(DateTime? upToDate = null)
        {
            return SystemTime.PeriodYears(DateOfEmployment, upToDate ?? SystemTime.Now);
        }
    }
}
