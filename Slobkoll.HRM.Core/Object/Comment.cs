namespace Slobkoll.HRM.Core.Object
{
    public class Comment
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
        /// Автор комментария
        /// </summary>
        public virtual User Author { get; set; }

        /// <summary>
        /// Номер подзадачи
        /// </summary>
        public virtual SubTask SubTask { get; set; }
    }
}
