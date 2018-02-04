using System;

namespace Domain.Interfaces
{
    public interface ISalaryService
    {
        double GetTotalSalary(DateTime? upToDate);
        double GetTotalSalaryWithoutSubordinatesPercents(DateTime? upToDate);
    }
}