using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;
using WebStore.Servicess.Interfaces;

namespace WebStore.Servicess
{
    public class InMemoryEmployeesData : IEmployeesData
    {

        private readonly List<Employee> _Employees = new()
        {
            new Employee { Id = 1, SurName = "Третьяк", Name = "Александр", MiddleName = "Иавнович", Age = 29 },
            new Employee { Id = 2, SurName = "Нестеренко", Name = "Сергей", MiddleName = "Геннадьевич", Age = 35 },
            new Employee { Id = 3, SurName = "Краус", Name = "Артур", MiddleName = "Артурович", Age = 30 },

        };

        public int Add(Employee employee)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Employee Get(int id)
        {
            _Employees.SingleOrDefault(employee => employee.Id == id);
        }

        public IEnumerable<Employee> GetAll() => _Employees;

        public void Update(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
