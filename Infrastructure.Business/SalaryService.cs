using System;
using Domain.Interfaces;

namespace Infrastructure.Business
{
    public class SalaryService : ISalaryService
    {
        public SalaryService(IEmployeeRepository repository)
        {
        }
        public double GetTotalSalary(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
