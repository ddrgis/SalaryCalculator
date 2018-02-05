using System.Collections.Generic;
using Domain.Interfaces;
using Infrastructure.Database;
using System.Web.Mvc;
using AutoMapper;
using Domain.Core.Interfaces;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeController()
        {
            _repository = new EmployeeRepository();
        }

        public EmployeeController(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            List<IEmployee> employees =_repository.List();
            List<EmployeeViewModel> employeeViewModels = Mapper.Map<List<IEmployee>, List<EmployeeViewModel>>(employees);
            return View(employeeViewModels);
        }

        public ActionResult Details(int id)
        {
            IEmployee employee = _repository.GetById(id);
            EmployeeViewModel viewModel = Mapper.Map<IEmployee, EmployeeViewModel>(employee);
            return View(viewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}