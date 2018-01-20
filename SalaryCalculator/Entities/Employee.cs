using System;
using System.Collections.Generic;
using System.Text;
using Domain.Core.Interfaces;
using Domain.Core.Services;

namespace Domain.Core.Entities
{
    public class Employee : Person
    {

        public Employee(double baseSalary, DateTime dateOfEmployment, double yearSalaryIncrement = 3)
        {
            BaseSalary = baseSalary;
            DateOfEmployment = dateOfEmployment;
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
