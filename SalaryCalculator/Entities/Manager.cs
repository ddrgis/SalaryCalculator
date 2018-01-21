using Domain.Core.Entities;
using Domain.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace Domain.Core
{
    public class Manager : Person
    {
        public Manager(double baseSalary, DateTime dateOfEmployment, double yearSalaryIncrement = 5, double maxYearIncrement = 40,
                      double subordinatesIncrement = 0.5, List<IEmployee> subordinates = null)
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
    }
}