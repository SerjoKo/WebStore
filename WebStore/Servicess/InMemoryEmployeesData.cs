using System;
using System.Collections.Generic;
using System.Linq;
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

        private int _MaxId;

        public InMemoryEmployeesData()
        {
            _MaxId = _Employees.Max(i => i.Id);
        }

        public int Add(Employee employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));

            if (_Employees.Contains(employee)) return employee.Id;

            employee.Id = ++_MaxId;

            _Employees.Add(employee);

            return employee.Id;
        }

        public bool Delete(int id)
        {
            var db_item = Get(id);

            if (db_item is null) return false;

            return _Employees.Remove(db_item);
        }

        public Employee Get(int id) => _Employees.SingleOrDefault(employee => employee.Id == id);

        public IEnumerable<Employee> GetAll() => _Employees;

        public void Update(Employee employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));

            if (_Employees.Contains(employee)) return;

            var db_item = Get(employee.Id);

            if (db_item is null) return;

            db_item.Name = employee.Name; // и т.д
            db_item.SurName = employee.SurName;
            db_item.MiddleName = employee.MiddleName;
            db_item.Age = employee.Age;
        }
    }
}
