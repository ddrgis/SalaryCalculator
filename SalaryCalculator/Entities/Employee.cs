using System;

namespace Domain.Core.Entities
{
    public class Employee : Person
    {
        [Obsolete("Constructor for ORM")]
        public Employee()
        {

        }

        public Employee(string firstName, string lastName, double baseSalary, DateTime dateOfEmployment, double percentageIncrementForYear = 3,
            double maxPercentageIncrementForYear = 30, int? superiorId = null)
            : base(firstName, lastName, baseSalary, dateOfEmployment, percentageIncrementForYear, maxPercentageIncrementForYear, superiorId: superiorId)
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