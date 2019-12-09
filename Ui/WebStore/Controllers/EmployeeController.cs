using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.DomainNew.ViewModels;
using WebStore.Infrasructure.Helpers;
using WebStore.Interfaces;

namespace WebStore.Controllers
{
    /// <summary>
    /// Контроллер для работы со списком сотрудников
    /// </summary>
    [Route("users")]
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Действие выводит весь список сотрудников
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(_employeeService.GetAll());
        }

        /// <summary>
        /// Действие выводит страницу с полной информацией о выбранном сотруднике
        /// </summary>
        /// <param name="id">id выбранного сотрудника</param>
        /// <returns></returns>
        [Route("{id}")]
        public IActionResult Details(int id)
        {
            var employeeView = _employeeService.GetById(id);

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
        [Route("Edit/{id?}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(int? id)
        {
            var employeeView = id.HasValue
                ? _employeeService.GetById(id.Value)
                : new EmployeeView();

            if (ReferenceEquals(employeeView, null))
                return NotFound();

            return View(employeeView);
        }

        /// <summary>
        /// Передает заполненные данные по сотруднику в форме
        /// </summary>
        /// <param name="employeeView">Модель сотрудника</param>
        /// <returns>Возвращает обновленный список сотрудников/returns>
        [HttpPost]
        [Route("Edit/{id?}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(EmployeeView employeeView)
        {
            const string ageOnHireDateError = "Возраст на дату найма не в пределах от 18 до 75 лет";
            const string cmpBirthAndHireDate = "Дата рождения после даты найма или они совпадают";

            var years = DateHelper.GetAge(
                employeeView.BirthDate, employeeView.HireDate);
            if (years < 18 || years > 75)
            {
                ModelState.AddModelError(
                    "Age", ageOnHireDateError);
            }

            if (employeeView.Age < years)
            {
                ModelState.AddModelError("Age", 
                    "Текущий возраст сотрудника меньше чем возраст на дату найма");
            }

            if (employeeView.BirthDate >= employeeView.HireDate)
            {
                ModelState.AddModelError("BirthDate", cmpBirthAndHireDate);
                ModelState.AddModelError("HireDate", cmpBirthAndHireDate);
            }


            if (!ModelState.IsValid)
                return View(employeeView);

            if (employeeView.Id == 0)
            {
                _employeeService.AddNew(employeeView);
                _employeeService.Commit();

                return RedirectToAction("Index");
            }

            var tmpEmployeeView = _employeeService.GetById(employeeView.Id);
            if (ReferenceEquals(tmpEmployeeView, null))
                return NotFound();

            tmpEmployeeView.Age = employeeView.Age;
            tmpEmployeeView.BirthDate = employeeView.BirthDate;
            tmpEmployeeView.FirstName = employeeView.FirstName;
            tmpEmployeeView.HireDate = employeeView.HireDate;
            tmpEmployeeView.Patronymic = employeeView.Patronymic;
            tmpEmployeeView.SurName = employeeView.SurName;

            _employeeService.Commit();

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Удаляет сотрудника
        /// </summary>
        /// <param name="id">id сотрудника</param>
        /// <returns>Возвращет</returns>
        [Route("delete/{id}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(int id)
        {
            var employeeView = _employeeService.GetById(id);
            if (!ReferenceEquals(employeeView, null))
                _employeeService.Delete(employeeView.Id);

            return RedirectToAction("Index");
        }
    }
}