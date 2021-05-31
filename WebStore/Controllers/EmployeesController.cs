using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebStore.Models;
using WebStore.Servicess.Interfaces;

namespace WebStore.Controllers
{   
    //[Route("Staf")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _EmployeesData;

        public EmployeesController(IEmployeesData EmployeesData)
        {
            _EmployeesData = EmployeesData;
        }

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
