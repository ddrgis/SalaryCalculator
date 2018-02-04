using System;

namespace Domain.Interfaces
{
    public interface ISalaryService
    {
        double GetTotalSalary(DateTime? upToDate);
    }
}