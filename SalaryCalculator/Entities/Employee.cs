using Domain.Core.Services;
using System;

namespace Domain.Core.Entities
{
    public class Employee : Person
    {
        [Obsolete("Constructor for ORM")]
        internal Employee()
        {
            
        }

        public Employee(double baseSalary, DateTime dateOfEmployment, double percentageIncrementForYear = 3,
            double maxPercentageIncrementForYear = 30)
            : base(baseSalary, dateOfEmployment, percentageIncrementForYear, maxPercentageIncrementForYear)
        {
        }

        public override double CountSalary(DateTime? payDate)
        {
            if (payDate == null)
            {
                payDate = SystemTime.Now;
            }

            return BaseSalary + CountSalaryIncrement(payDate);
        }
    }
}