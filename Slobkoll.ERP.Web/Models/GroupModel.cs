using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Slobkoll.ERP.Web.Models
{
    public class GroupCreateModel
    {
        [Required(ErrorMessage = "Введите Название")]
        [Display(Name = "Название*")]
        public string Name { get; set; }

        [Display(Name = "Список группы")]
        public int[] GroupUser { get; set; }
    }
}