using System;

namespace Slobkoll.HRM.Core.Object
{
    public class Comments
    {
        /// <summary>
        /// Ид комментария
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Текст комментария
        /// </summary>
        public virtual string TextComment { get; set; }

        /// <summary>
        /// Дата и время создания комментария
        /// </summary>
        public virtual DateTime DateTime { get; set; }

        /// <summary>
        /// Автор комментария
        /// </summary>
        public virtual User Author { get; set; }

        /// <summary>
        /// Номер подзадачи
        /// </summary>
        public virtual SubTask SubTask { get; set; }
    }
}
