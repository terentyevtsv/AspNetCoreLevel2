using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Infrasructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Infrasructure.Services
{
    /// <summary>
    /// Класс реализующий интерфейс сотрудника в памяти приложения
    /// </summary>
    public class MemoryEmployeeService : IEmployeeService
    {
        private readonly List<EmployeeView> _employees;

        public MemoryEmployeeService()
        {
            _employees = new List<EmployeeView>
            {
                new EmployeeView
                {
                    Id = 1,
                    SurName = "Терентьев",
                    FirstName = "Сергей",
                    Patronymic = "Викторович",
                    Age = 29,
                    BirthDate = new DateTime(1989, 9, 21),
                    HireDate = new DateTime(2011, 12, 5)
                },
                new EmployeeView
                {
                    Id = 2,
                    SurName = "Иванов",
                    FirstName = "Андрей",
                    Patronymic = "Владимирович",
                    Age = 35,
                    BirthDate = new DateTime(1984, 8, 1),
                    HireDate = new DateTime(2009, 6, 1)
                }
            };
        }

        public IEnumerable<EmployeeView> GetAll()
        {
            return _employees;
        }

        public EmployeeView GetById(int id)
        {
            var employeeView = _employees
                .SingleOrDefault(emp => emp.Id.Equals(id));
            return employeeView;
        }

        public void Commit()
        {
            
        }

        public void AddNew(EmployeeView model)
        {
            var id = _employees.Any()
                ? _employees.Max(emp => emp.Id) + 1
                : 1;
            model.Id = id;

            _employees.Add(model);
        }

        public void Delete(int id)
        {
            var employee = GetById(id);
            if (!ReferenceEquals(employee, null))
                _employees.Remove(employee);
        }
    }
}
