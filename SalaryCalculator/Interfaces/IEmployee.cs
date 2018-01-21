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
        double SubordinateIncrement { get; set; }

        double CountSalary(DateTime? payDate);
        double GetSubordinatesIncrement(DateTime? upToDate);
    }
}