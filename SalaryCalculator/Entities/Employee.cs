using System;

namespace Domain.Core.Entities
{
    public class Employee : Person
    {
        public Employee(double baseSalary, DateTime dateOfEmployment, double yearSalaryIncrement = 3, double maxYearIncrement = 30)
        {
            BaseSalary = baseSalary;
            DateOfEmployment = dateOfEmployment;
            YearSalaryIncrement = yearSalaryIncrement;
            MaxYearIncrement = maxYearIncrement;
        }

        public override double CountSalary(DateTime? payDate)
        {
            if (payDate == null)
            {
                payDate = DateTime.Now;
            }

            return BaseSalary + CountSalaryIncrement(payDate);
        }
    }
}