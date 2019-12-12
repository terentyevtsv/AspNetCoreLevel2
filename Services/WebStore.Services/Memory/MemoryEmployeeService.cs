using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.DomainNew.ViewModels;
using WebStore.Interfaces;

namespace WebStore.Services.Memory
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

        public EmployeeView UpdateEmployee(int id, EmployeeView entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var employee = _employees.SingleOrDefault(e => e.Id.Equals(id));
            if (employee == null)
                throw new InvalidOperationException("Employee not exists");

            employee.Age = entity.Age;
            employee.BirthDate = entity.BirthDate;
            employee.FirstName = entity.FirstName;
            employee.Patronymic = entity.Patronymic;
            employee.SurName = entity.SurName;
            employee.Position = entity.Position;
            employee.HireDate = entity.HireDate;

            return employee;
        }

        public void Commit()
        {
            
        }

        public void AddNew(EmployeeView model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

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
