using System;
using System.ComponentModel.DataAnnotations;

namespace WebStore.DomainNew.ViewModels
{
    public class EmployeeView
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [Display(Name = "Имя")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Отсутствует имя")]
        [StringLength(25, ErrorMessage = "Имя должно состоять минимум из двух символов, максимум из 25 символов", MinimumLength = 2)]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Display(Name = "Фамилия")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Отсутствует фамилия")]
        [StringLength(50, ErrorMessage = "Фамилия должна состоять минимум из двух символов, максимум из 50 символов", MinimumLength = 2)]
        public string SurName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        /// <summary>
        /// Возраст
        /// </summary>
        [Display(Name = "Возраст")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Отсутствует возраст")]
        public int Age { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Дата приема на работу
        /// </summary>
        [Display(Name = "Дата найма")]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }
    }
}
