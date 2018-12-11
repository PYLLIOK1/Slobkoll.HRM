namespace Slobkoll.HRM.Core.Object
{
    public class SubTask
    {
        /// <summary>
        /// Номер подзадачи
        /// </summary>
        public virtual int Id { get; set; }

        public virtual Task TaskId { get; set; }

        /// <summary>
        /// Путь к файлу
        /// </summary>
        public virtual string Path { get; set; }

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
