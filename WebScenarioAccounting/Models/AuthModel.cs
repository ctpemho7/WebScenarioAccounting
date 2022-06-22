using System.ComponentModel.DataAnnotations;

namespace WebScenarioAccounting.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }

    public class RegisterModel
    {
        public int id { get; set; }

        [Required]
        [Display(Name = "Паспорт")]
        public string Passport { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Required]
        [Display(Name = "Дата рождения")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public System.DateTime Birthdate { get; set; }

        [Required]
        [Display(Name = "Пол")]
        public int Sex { get; set; }

        [Required]
        [Display(Name = "Тип пользователя")]
        public int UserType { get; set; }


        [Required]
        [Display(Name = "Пол")]
        public virtual ClassifierSex ClassifierSex { get; set; }

        [Required]
        [Display(Name = "Тип пользователя")]
        public virtual ClassifierUserType ClassifierUserType { get; set; }




        [Required]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

    }
}