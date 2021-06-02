using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Data
{
    public static class TastData
    {
        public static List<Employee> Employees { get; } = new()
        {
            new Employee { Id = 1, SurName = "Третьяк", Name = "Александр", MiddleName = "Иавнович", Age = 29 },
            new Employee { Id = 2, SurName = "Нестеренко", Name = "Сергей", MiddleName = "Геннадьевич", Age = 35 },
            new Employee { Id = 3, SurName = "Краус", Name = "Артур", MiddleName = "Артурович", Age = 30 },

        };
    }
}
