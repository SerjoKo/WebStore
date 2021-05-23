using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private static readonly List<Employee> _Employees = new()
        {
            new Employee { Id = 1, Surname = "Третьяк", Name = "Александр",  MiddleName = "Иавнович",  Age = 29 },
            new Employee { Id = 2, Surname = "Нестеренко", Name = "Сергей",  MiddleName = "Геннадьевич", Age = 35 },
            new Employee { Id = 3, Surname = "Краус", Name = "Артур",  MiddleName = "Артурович", Age = 30 },

        };
        private readonly IConfiguration _Configuration; 

        public HomeController(IConfiguration Configuration)
        {
            _Configuration = Configuration;
        }
        public IActionResult Index()
        {
            //return Content("Тест контроллера!");
            return View();
        }

        public IActionResult SecondAction()
        {
            return Content(_Configuration["Creatings"]);
        }

        public IActionResult Employees()
        {
            return View(_Employees);
        }
    }
}
