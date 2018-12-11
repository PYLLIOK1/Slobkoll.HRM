using Slobkoll.HRM.Core.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Slobkoll.HRM.Web.Models
{
    public class TaskCreateModel
    {
        [Required(ErrorMessage = "Введите Название")]
        [Display(Name = "Название задачи")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите описание")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        public string Path { get; set; }

        public DateTime DateBegin { get; set; }

        [Required(ErrorMessage = "Введите дату окончания")]
        [Display(Name = "Дата окончания")]
        public DateTime DateEnd { get; set; }

        public User Author { get; set; }

        [Required(ErrorMessage = "Введите статус")]
        [Display(Name = "Статус задачи")]
        public string Status { get; set; }

        public bool Change { get; set; }

        public  IList<SubTask> SubTask { get; set; }
    }
}