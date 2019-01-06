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
        /// файл
        /// </summary>
        public virtual byte[] Files { get; set; }

        /// <summary>
        /// Название файла
        /// </summary>
        public virtual string Name { get; set; }

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
    }
}
