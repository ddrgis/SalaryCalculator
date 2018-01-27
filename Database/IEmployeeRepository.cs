using System.Collections.Generic;
using Domain.Core.Interfaces;

namespace Infrastructure.Database
{
    public interface IEmployeeRepository
    {
        List<IEmployee> List();
        IEmployee GetById(int id);
        void Add(IEmployee entity);
        void Delete(IEmployee entity);
        void Edit(IEmployee entity);
    }
}