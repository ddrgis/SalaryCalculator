using System;
using System.Linq;
using Domain.Core;
using Domain.Core.Interfaces;
using Domain.Interfaces;
using Infrastructure.Database;

namespace Infrastructure.Business
{
    public class SalaryService : ISalaryService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IEmployeeService _employeeService;


        //todo: internal?
        public SalaryService(IEmployeeRepository repository, IEmployeeService employeeService)
        {
            _employeeService = employeeService ?? new EmployeeService();
            _repository = repository ?? new EmployeeRepository();
        }

        public double GetTotalSalary(DateTime? upToDate)
        {
            if (upToDate == null)
            {
                upToDate = SystemTime.Now;
            }

            IEmployee rootEmployee = _employeeService.BuildTree();
            double rootEmployeeSalary = rootEmployee.CountSalary(upToDate);

            return rootEmployee.Subordinates?.Aggregate(rootEmployeeSalary, CountSalaryIterator) ?? rootEmployeeSalary;

            double CountSalaryIterator(double acc, IEmployee currentEmployee)
            {
                if (currentEmployee.Subordinates == null)
                {
                    return acc + currentEmployee.CountSalary(upToDate);
                }

                return currentEmployee.CountSalary(upToDate) +
                       currentEmployee.Subordinates.Aggregate(acc, CountSalaryIterator);
            }
        }

        //todo: delete this method (YAGNI)
        public double GetTotalSalaryWithoutSubordinatesPercents(DateTime? upToDate)
        {
            if (upToDate == null)
            {
                upToDate = SystemTime.Now;
            }

            return _repository.List().Aggregate(0.0, (sum, employee) => sum + employee.CountSalary(upToDate));
        }
    }
}
