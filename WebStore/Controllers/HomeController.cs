using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Models;

namespace WebStore.Controllers
{
    /// <summary>
    /// Контроллер для работы со списком сотрудников
    /// </summary>
    public class HomeController : Controller
    {
        private readonly List<EmployeeView> _employeeViews;

        public HomeController()
        {
            // Заготовленный список сотрудников
            _employeeViews = new List<EmployeeView>
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

        /// <summary>
        /// Действие выводит весь список сотрудников
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View(_employeeViews);
        }

        /// <summary>
        /// Действие выводит страницу с полной информацией о выбранном сотруднике
        /// </summary>
        /// <param name="id">id выбранного сотрудника</param>
        /// <returns></returns>
        public IActionResult Details(int id)
        {
            var employeeView = _employeeViews
                .SingleOrDefault(emp => emp.Id == id);

            return View(employeeView);
        }        
    }
}