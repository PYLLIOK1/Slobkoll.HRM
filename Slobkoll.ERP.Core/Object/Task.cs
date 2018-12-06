using System;
using System.Collections.Generic;

namespace Slobkoll.ERP.Core.Object
{
    public class Task
    {
        /// <summary>
        /// Номер задачи
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Название задачи
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Описание задачи
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Путь к файлу
        /// </summary>
        public virtual string Path { get; set; }

        /// <summary>
        /// Время Начала задачи
        /// </summary>
        public virtual DateTime DateBegin { get; set; }

        /// <summary>
        /// Время окончания задачи
        /// </summary>
        public virtual DateTime DateEnd { get; set; }

        /// <summary>
        /// Автор задачи
        /// </summary>
        public virtual User Author { get; set; }

        /// <summary>
        /// Статус задачи
        /// </summary>
        public virtual string Status { get; set; }

        /// <summary>
        /// Доступнали задача
        /// </summary>
        public virtual bool Change { get; set; }

        private IList<SubTask> subtask;
        /// <summary>
        /// Список подзадачи
        /// </summary>
        public virtual IList<SubTask> SubTask
        {
            get { return subtask ?? (subtask = new List<SubTask>()); }
            set { subtask = value; }
        }
    }
}

