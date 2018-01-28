using Domain.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace Domain.Core.Entities
{
    public class Manager : Person
    {
        [Obsolete("Constructor for ORM")]
        public Manager()
        {
        }

        public Manager(string firstName, string lastName, double baseSalary, DateTime dateOfEmployment, double percentageIncrementForYear = 5, double maxPercentageIncrementForYear = 40,
                      double percentagesIncrementFromSubordinates = 0.5, List<IEmployee> subordinates = null, int? superiorId = null)
           : base(firstName, lastName, baseSalary, dateOfEmployment, percentageIncrementForYear, maxPercentageIncrementForYear,
               percentagesIncrementFromSubordinates, subordinates, superiorId)
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