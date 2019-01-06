using Slobkoll.HRM.Core.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

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

        [Required(ErrorMessage = "выберете дату")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy hh'/'mm }", ApplyFormatInEditMode = true )]
        [Display(Name = "Дата Окончиния")]
        public DateTime DateTime { get; set; }

        [Display(Name = "Список подчинненных")]
        public int[] UserIdPerfomers { get; set; }

        [Display(Name = "Список групп подчиненных")]
        public int[] UserIdPerfomerGroup { get; set; }

        public HttpPostedFileBase File { get; set; }
    }
}