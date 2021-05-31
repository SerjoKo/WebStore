using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebStore.Models;
using WebStore.Servicess.Interfaces;
using WebStore.ViewModels;

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
            var employee = _EmployeesData.Get(id);//_Employees.FirstOrDefault(employe => employe.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        //[Route("all")]
        public IActionResult Index()
        {
            return View(_EmployeesData.GetAll());
        }
        
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            var employee = _EmployeesData.Get(id);
            
            if (employee is null) return NotFound();

            var view_model = new EmployeeViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                SurName = employee.SurName,
                MiddleName = employee.MiddleName,
                Age = employee.Age,
            };
            return View(view_model); 
        }

        [HttpPost]
        public IActionResult Edit(EmployeeViewModel Model)
        {
            var employee = new Employee
            {
                Id = Model.Id,
                Name = Model.Name,
                SurName = Model.SurName,
                MiddleName = Model.MiddleName,
                Age = Model.Age,
            };

            _EmployeesData.Update(employee);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            return View();
        }
    }

    
}
