using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Core.Interfaces;

namespace Domain.Core.Entities
{
    public class Salesman : Person
    {
        [Obsolete("Constructor for ORM")]
        public Salesman()
        {

        }


        public Salesman(double baseSalary, DateTime dateOfEmployment, double percentageIncrementForYear = 1,
            double maxPercentageIncrementForYear = 35, double percentagesIncrementFromSubordinates = 0.3, List<IEmployee> subordinates = null)
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

        public override double GetIncrementFromSubordinates(DateTime? upToDate)
        {
            if (Subordinates == null)
            {
                return 0;
            }

            double allSubordinatesSalary = Subordinates.Aggregate(0.0, Iterator);

            return allSubordinatesSalary * PercentageIncrementFromSubordinates / 100;

            double Iterator(double accamulator, IEmployee currentSubordinate)
            {
                if (currentSubordinate.Subordinates != null)
                {
                    return currentSubordinate.CountSalary(upToDate) + currentSubordinate.Subordinates.Aggregate(accamulator, Iterator);
                }

                return accamulator + currentSubordinate.CountSalary(upToDate);
            }
        }
    }
}