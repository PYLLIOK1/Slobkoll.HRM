using System.Collections.Generic;

namespace Slobkoll.HRM.Core.Object
{
    public class SubTask
    {
        /// <summary>
        /// Номер подзадачи
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Номер Задачи
        /// </summary>
        public virtual Task TaskId { get; set; }

        /// <summary>
        /// путь к файлу
        /// </summary>
        public virtual string Files { get; set; }

        /// <summary>
        /// Название файла
        /// </summary>
        public virtual string FileName { get; set; }

        /// <summary>
        /// Статус подзадачи
        /// </summary>
        public virtual string Status { get; set; }

        /// <summary>
        /// Исполнитель
        /// </summary>
        public virtual User Performer { get; set; }

        /// <summary>
        /// Информирование автора
        /// </summary>
        public virtual bool ChangeAuthor { get; set; }

        /// <summary>
        /// Информирование исполнителя
        /// </summary>
        public virtual bool ChangePerformer { get; set; }

        private IList<Comments> comments;
        /// <summary>
        /// Список комментарий
        /// </summary>
        public virtual IList<Comments> Comments
        {
            get { return comments ?? (comments = new List<Comments>()); }
            set { comments = value; }
        }
        private IList<SubTaskMod> subTaskMods;
        /// <summary>
        /// список изменений
        /// </summary>
        public virtual IList<SubTaskMod> SubTaskMods
        {
            get { return subTaskMods ?? (subTaskMods = new List<SubTaskMod>()); }
            set { subTaskMods = value; }
        }
    }
}
