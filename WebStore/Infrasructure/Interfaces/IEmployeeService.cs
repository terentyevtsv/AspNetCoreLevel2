using System.Collections.Generic;
using WebStore.ViewModels;

namespace WebStore.Infrasructure.Interfaces
{
    /// <summary>
    /// Интерфейс для работы с сотрудниками
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// Список сотрудников
        /// </summary>
        /// <returns></returns>
        IEnumerable<EmployeeView> GetAll();

        /// <summary>
        /// Получение сотрудника по id
        /// </summary>
        /// <param name="id">id сотрудника</param>
        /// <returns></returns>
        EmployeeView GetById(int id);

        /// <summary>
        /// Сохранить изменения
        /// </summary>
        void Commit();

        /// <summary>
        /// Добавить нового сотрудника
        /// </summary>
        /// <param name="model"></param>
        void AddNew(EmployeeView model);

        /// <summary>
        /// Удалить сотрудника по его id
        /// </summary>
        /// <param name="id">id</param>
        void Delete(int id);
    }
}
