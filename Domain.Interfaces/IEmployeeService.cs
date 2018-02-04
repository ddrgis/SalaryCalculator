using Domain.Core.Interfaces;

namespace Domain.Interfaces
{
    public interface IEmployeeService
    {
        IEmployee BuildTree(int? rootId = null);
    }
}