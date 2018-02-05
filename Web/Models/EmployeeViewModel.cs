using System;

namespace Web.Models
{
    public class EmployeeViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double BaseSalary { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public double PercentageIncrementForYear { get; set; }
        public double MaxPercentageIncrementForYear { get; set; }
        public double PercentageIncrementFromSubordinates { get; set; }
    }
}