using Slobkoll.ERP.Core.Object;
using Slobkoll.ERP.Core.Repository.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Slobkoll.ERP.Web.Models
{
    public class UserCreateModel
    {
        [Required(ErrorMessage = "Введите логин")]
        [Display(Name = "Логин*")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [Display(Name = "Пароль*")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [Display(Name = "Повторите пароль*")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Введите ФИО")]
        [Display(Name = "ФИО*")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите должность")]
        [Display(Name = "Должность*")]
        public string Position { get; set; }

        [Display(Name = "Список групп")]
        public int[] IdGroup { get; set; }

        [Display(Name = "Список подчинненных")]
        public int[] UserIdPerfomers { get; set; }

        [Display(Name = "Список заказчиков")]
        public int[] UserIdCustomer { get; set; }

        [Display(Name = "Список наблюдателей")]
        public int[] UserIdObserver { get; set; }

        [Display(Name = "Список наблюдаемых")]
        public int[] UserIdObserved { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "Введите логин")]
        [Display(Name = "Логин*")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [Display(Name = "Пароль*")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class UserEditModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите логин")]
        [Display(Name = "Логин*")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [Display(Name = "Пароль*")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Введите ФИО")]
        [Display(Name = "ФИО*")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите должность")]
        [Display(Name = "Должность*")]
        public string Position { get; set; }

        [Display(Name = "Админ права")]
        public bool AdminRole { get; set; }

        [Display(Name = "Статус пользователя")]
        public bool StatusUser { get; set; }

        [Display(Name = "Список групп")]
        public int[] IdGroup { get; set; }

        [Display(Name = "Список подчинненных")]
        public int[] UserIdPerfomer { get; set; }

        [Display(Name = "Список заказчиков")]
        public int[] UserIdCustomer { get; set; }

        [Display(Name = "Список наблюдателей")]
        public int[] UserIdObserver { get; set; }

        [Display(Name = "Список наблюдаемых")]
        public int[] UserIdObserved { get; set; }
    }
}