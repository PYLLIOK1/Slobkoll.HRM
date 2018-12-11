using Slobkoll.HRM.Core.Object;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Slobkoll.HRM.Web.Models
{
    public class GroupCreateModel
    {
        [Required(ErrorMessage = "Введите Название")]
        [Display(Name = "Название*")]
        public string Name { get; set; }

        [Display(Name = "Список группы")]
        public int[] GroupUser { get; set; }
    }
    public class GroupEditModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите Название")]
        [Display(Name = "Название*")]
        public string Name { get; set; }

        [Display(Name = "Список группы")]
        public int[] GroupUser { get; set; }
    }
    public class GroupDeleteModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
    public class GroupDetailsModel
    {
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Список группы")]
        public IList<User> GroupUser { get; set; }
    }
}