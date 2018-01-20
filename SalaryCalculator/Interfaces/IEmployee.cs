using System;
using System.Collections.Generic;

namespace Domain.Core.Interfaces
{
    public interface IEmployee
    {
        double BaseSalary { get; set; }
        DateTime DateOfEmployment { get; set; }
        double YearSalaryIncrement { get; set; }
        List<IEmployee> Subordinates { get; set; }
        double SubordinatesIncrement { get; set; }

        double CountSalary(DateTime? payDate);
    }
}