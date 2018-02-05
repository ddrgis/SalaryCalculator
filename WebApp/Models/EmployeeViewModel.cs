using System;

namespace WebApp.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public int SuperiorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double BaseSalary { get; set; }
        public DateTime DateOfEmployment { get; set; }
        public double PercentageIncrementForYear { get; set; }
        public double MaxPercentageIncrementForYear { get; set; }
        public double PercentageIncrementFromSubordinates { get; set; }
    }
}