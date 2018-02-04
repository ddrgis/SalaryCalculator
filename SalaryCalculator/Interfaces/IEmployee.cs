using System;
using System.Collections.Generic;

namespace Domain.Core.Interfaces
{
    public interface IEmployee
    {
        int Id { get; set; }
        int? SuperiorId { get; set; }
        double BaseSalary { get; set; }
        DateTime DateOfEmployment { get; set; }
        double PercentageIncrementForYear { get; set; }
        List<IEmployee> Subordinates { get; set; }
        double PercentageIncrementFromSubordinates { get; set; }

        double CountSalary(DateTime? payDate = null);
        double GetIncrementFromSubordinates(DateTime? upToDate = null);
    }
}