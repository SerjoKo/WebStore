using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebStore.Models;

namespace WebStore.Controllers
{   
    //[Route("Staf")]
    public class EmployeesController : Controller
    {
        
        private static readonly List<Employee> _Employees = new()
        {
            new Employee { Id = 1, SurName = "Третьяк", Name = "Александр", MiddleName = "Иавнович", Age = 29 },
            new Employee { Id = 2, SurName = "Нестеренко", Name = "Сергей", MiddleName = "Геннадьевич", Age = 35 },
            new Employee { Id = 3, SurName = "Краус", Name = "Артур", MiddleName = "Артурович", Age = 30 },

        };

        //[Route("info-id-{id}")]
        public IActionResult Details(int id)
        {
            var employee = _Employees.FirstOrDefault(employe => employe.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        //[Route("all")]
        public IActionResult Index()
        {
            return View(_Employees);
        }
    }
}
