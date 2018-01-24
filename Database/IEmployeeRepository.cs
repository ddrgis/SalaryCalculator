using Domain.Core.Interfaces;

namespace Database
{
    public interface IEmployeeRepository
    {
        IEmployee FindById(int id);
    }
}