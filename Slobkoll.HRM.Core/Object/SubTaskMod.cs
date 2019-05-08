using System;

namespace Slobkoll.HRM.Core.Object
{
    public class SubTaskMod
    {
        /// <summary>
        /// Номер
        /// </summary>
        public virtual int Id { get; set; }
        /// <summary>
        /// Номер подззадачи
        /// </summary>
        public virtual SubTask SubTaskId { get; set; }
        /// <summary>
        /// Статус изменения
        /// </summary>
        public virtual string Status { get; set; }
        /// <summary>
        /// Время изменения
        /// </summary>
        public virtual DateTime DateTime { get; set; }
    }
}
