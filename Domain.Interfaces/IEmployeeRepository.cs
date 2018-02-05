using System.Collections.Generic;
using System.Data;
using Domain.Core.Interfaces;

namespace Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        List<IEmployee> List();

        IEmployee GetById(int id);

        void Add(IEmployee entity);

        void Delete(IEmployee entity);

        void Edit(IEmployee entity, IDbConnection connection = null);
    }
}