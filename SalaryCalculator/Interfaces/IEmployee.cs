using System;

namespace Domain.Core.Interfaces
{
    public interface IEmployee
    {
        double BaseSalary { get; set; }
        DateTime DateOfEmployment { get; set; }
        double YearSalaryIncrement { get; set; }

        double CountSalary(DateTime? payDate);
    }
}