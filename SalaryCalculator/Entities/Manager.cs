using Domain.Core.Interfaces;
using Domain.Core.Services;
using System;
using System.Collections.Generic;

namespace Domain.Core.Entities
{
    public class Manager : Person
    {
        [Obsolete("Constructor for ORM")]
        internal Manager()
        {

        }

        public Manager(double baseSalary, DateTime dateOfEmployment, double percentageIncrementForYear = 5, double maxPercentageIncrementForYear = 40,
                      double percentagesIncrementFromSubordinates = 0.5, List<IEmployee> subordinates = null)
           : base(baseSalary, dateOfEmployment, percentageIncrementForYear, maxPercentageIncrementForYear, percentagesIncrementFromSubordinates, subordinates)
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