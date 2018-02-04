using Domain.Core.Interfaces;
using Domain.Interfaces;
using Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Business
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService()
        {
            _repository = new EmployeeRepository();
        }

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public IEmployee BuildTree(int? rootId = null)
        {
            List<IEmployee> listOfEmployees = _repository.List();
            return BuildTree(listOfEmployees, rootId);
        }

        public IEmployee BuildTree(List<IEmployee> listOfEmployees, int? rootId = null)
        {
            IEmployee boss;
            try
            {
                boss = rootId == null ? listOfEmployees.First(em => em.SuperiorId == null) : listOfEmployees.First(em => em.Id == rootId);
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException("Не удалось получить список сотрудников", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new ArgumentNullException($"Не удалось построить дерево работников, т.к. в базе нет сотрудника с Id={rootId}", ex);
            }

            return AddSubordinatesRecursive(boss);

            IEmployee AddSubordinatesRecursive(IEmployee currentRoot)
            {
                IEmployee[] subordinates = listOfEmployees.Where(em => em.SuperiorId == boss.Id).Select(em => em).ToArray();
                if (!subordinates.Any())
                {
                    return currentRoot;
                }

                foreach (IEmployee subordinate in subordinates)
                {
                    if (subordinate.Subordinates == null)
                    {
                        if (currentRoot.Subordinates == null)
                        {
                            currentRoot.Subordinates = new List<IEmployee>() { subordinate };
                        }
                        else
                        {
                            currentRoot.Subordinates.Add(subordinate);
                        }
                    }
                    else
                    {
                        AddSubordinatesRecursive(subordinate);
                    }
                }
                return currentRoot;
            }
        }
    }
}