using AutoMapper;
using Domain.Core.Interfaces;
using System.Web.Mvc;
using System.Web.Routing;
using WebApp.Models;

namespace WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Mapper.Initialize(cfg => cfg.CreateMap<IEmployee, EmployeeViewModel>());
        }
    }
}