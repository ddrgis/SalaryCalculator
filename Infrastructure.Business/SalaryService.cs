using System;
using System.Linq;
using Domain.Core;
using Domain.Interfaces;
using Infrastructure.Database;

namespace Infrastructure.Business
{
    public class SalaryService : ISalaryService
    {
        private readonly IEmployeeRepository _repository;

        public SalaryService(IEmployeeRepository repository)
        {
            _repository = repository ?? new EmployeeRepository();
        }

        public double GetTotalSalary(DateTime? date)
        {
            if (date == null)
            {
                date = SystemTime.Now;
            }

            return _repository.List().Aggregate(0.0, (sum, employee) => sum + employee.CountSalary(date));
        }
    }
}
