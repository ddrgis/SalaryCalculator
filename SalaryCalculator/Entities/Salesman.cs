using Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Core.Entities
{
    public class Salesman : Person
    {
        public Salesman(double baseSalary, DateTime dateOfEmployment, double yearSalaryIncrement = 1, double maxYearIncrement = 35,
                        double subordinatesIncrement = 0.3, List<IEmployee> subordinates = null)
            : base(baseSalary, dateOfEmployment, yearSalaryIncrement, maxYearIncrement, subordinatesIncrement, subordinates)
        {
        }

        public override double CountSalary(DateTime? payDate)
        {
            if (payDate == null)
            {
                payDate = DateTime.Now;
            }

            return BaseSalary + CountSalaryIncrement(payDate);
        }

        public override double GetSubordinatesIncrement(DateTime? upToDate)
        {
            if (Subordinates == null)
            {
                return 0;
            }

            double allSubordinatesSalary = Subordinates.Aggregate(0.0, Iter);

            return allSubordinatesSalary * SubordinateIncrement / 100;

            double Iter(double acc, IEmployee currentSubordinate)
            {
                if (currentSubordinate.Subordinates != null)
                {
                    return currentSubordinate.CountSalary(upToDate) + currentSubordinate.Subordinates.Aggregate(acc, Iter);
                }

                return acc + currentSubordinate.CountSalary(upToDate);
            }
        }
    }
}