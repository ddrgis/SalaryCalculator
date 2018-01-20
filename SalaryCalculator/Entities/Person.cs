using System;
using System.Xml.Schema;
using Domain.Core.Interfaces;
using Domain.Core.Services;

namespace Domain.Core.Entities
{
    public abstract class Person : IEmployee
    {
        public DateTime DateOfEmployment { get; set; }
        public double BaseSalary { get; set; }
        public double YearSalaryIncrement { get; set; }
        public double MaxYearIncrement { get; set; }

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
