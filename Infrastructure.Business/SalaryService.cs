using System;
using System.Linq;
using Domain.Core;
using Domain.Interfaces;
using Infrastructure.Database;

namespace Infrastructure.Business
{
    public class SalaryService : ISalaryService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public SalaryService(IEmployeeRepository repository = null)
        {
            _employeeRepository = repository ?? new EmployeeRepository();
        }

        public double GetTotalSalary(DateTime? date = null)
        {
            if (date == null)
            {
                date = SystemTime.Now;
            }

            var employees = _employeeRepository.List();
            return employees.Aggregate(0.0, ((d, employee) => employee.CountSalary(date)));
        }
    }
}
