using System;
using System.Collections.Generic;

namespace Domain.Core.Interfaces
{
    public interface IEmployee
    {
        double BaseSalary { get; }
        DateTime DateOfEmployment { get; }
        double PercentageIncrementForYear { get; }
        List<IEmployee> Subordinates { get; }
        double PercentageIncrementFromSubordinates { get; }

        double CountSalary(DateTime? payDate = null);
        double GetIncrementFromSubordinates(DateTime? upToDate = null);
    }
}