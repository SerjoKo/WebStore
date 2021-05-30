using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebStore.Models;

namespace WebStore.Controllers
{   
    //[Route("Staf")]
    public class EmployeesController : Controller
    {
        public EmployeesController()
        {

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
