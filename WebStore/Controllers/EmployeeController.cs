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
    public class EmployeeController : Controller
    {
        private static readonly List<EmployeeView> _employeeViews
            = new List<EmployeeView>
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

            if (ReferenceEquals(employeeView, null))
                return NotFound();

            return View(employeeView);
        }

        /// <summary>
        /// Действие возвращает пустую форму нового сотрудника если не передается id
        /// Иначе ищет сотрудника и возвращает представление с формой по сотруднику для изменения
        /// </summary>
        /// <param name="id">id сотрудника</param>
        /// <returns></returns>
        public IActionResult Edit(int? id)
        {
            var employeeView = id.HasValue
                ? _employeeViews
                    .SingleOrDefault(emp => emp.Id == id.Value)
                : new EmployeeView();

            return View(employeeView);
        }

        /// <summary>
        /// Передает заполненные данные по сотруднику в форме
        /// </summary>
        /// <param name="employeeView">Модель сотрудника</param>
        /// <returns>Возвращает обновленный список сотрудников/returns>
        [HttpPost]
        public IActionResult Edit(EmployeeView employeeView)
        {
            if (employeeView.Id == 0)
            {
                var id = _employeeViews.Any()
                    ? _employeeViews.Max(emp => emp.Id) + 1
                    : 1;
                employeeView.Id = id;

                _employeeViews.Add(employeeView);

                return RedirectToAction("Index");
            }

            var tmpEmployeeView = _employeeViews
                .SingleOrDefault(emp => emp.Id == employeeView.Id);
            if (ReferenceEquals(tmpEmployeeView, null))
                return NotFound();

            tmpEmployeeView.Age = employeeView.Age;
            tmpEmployeeView.BirthDate = employeeView.BirthDate;
            tmpEmployeeView.FirstName = employeeView.FirstName;
            tmpEmployeeView.HireDate = employeeView.HireDate;
            tmpEmployeeView.Patronymic = employeeView.Patronymic;
            tmpEmployeeView.SurName = employeeView.SurName;

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Удаляет сотрудника
        /// </summary>
        /// <param name="id">id сотрудника</param>
        /// <returns>Возвращет</returns>
        public IActionResult Delete(int id)
        {
            var employeeView = _employeeViews
                .SingleOrDefault(emp => emp.Id == id);
            if (!ReferenceEquals(employeeView, null))
                _employeeViews.Remove(employeeView);

            return RedirectToAction("Index");
        }
    }
}