using Domain.Core.Interfaces;
using System;

namespace Domain.Core.Factories
{
    public static class EmployeeFactory
    {
        public static IEmployee CreateEmployee(string employeeType, params object[] args)
        {
            try
            {
                Type type = Type.GetType("Domain.Core.Entities." + employeeType);
                return type != null ? (IEmployee)Activator.CreateInstance(type, args) : null;
            }
            catch (MissingMemberException)
            {
                return null;
            }
        }
    }
}