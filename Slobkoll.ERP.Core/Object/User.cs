using System.Collections.Generic;

namespace Slobkoll.ERP.Core.Object
{
    public class User
    {

        public virtual int Id { get; set; }


        public virtual string Login { get; set; }


        public virtual string Password { get; set; }


        public virtual string Name { get; set; }


        public virtual string Position { get; set; }


        public virtual bool AdminRole { get; set; }


        public virtual bool StatusUser { get; set; }

        private IList<Group> group;
        /// <summary>
        /// Список групп
        /// </summary>
        public virtual IList<Group> Group
        {
            get { return group ?? (group = new List<Group>()); }
            set { group = value; }
        }

        private IList<User> userObserved;
        /// <summary>
        /// за кем наблюдает
        /// </summary>
        public virtual IList<User> UserObserved
        {
            get { return userObserved ?? (userObserved = new List<User>()); }
            set { userObserved = value; }
        }

        private IList<User> userObserver;
        /// <summary>
        /// кто наблюдает за вами
        /// </summary>
        public virtual IList<User> UserObserver
        {
            get { return userObserver ?? (userObserver = new List<User>()); }
            set { userObserver = value; }
        }


        private IList<User> userPerformer;
        /// <summary>
        /// Кому может дать задачи
        /// </summary>
        public virtual IList<User> UserPerformer
        {
            get { return userPerformer ?? (userPerformer = new List<User>()); }
            set { userPerformer = value; }
        }

        private IList<User> userCustomer;
        /// <summary>
        /// Кто может дать задание
        /// </summary>
        public virtual IList<User> UserCustomer
        {
            get { return userCustomer ?? (userCustomer = new List<User>()); }
            set { userCustomer = value; }
        }

        private IList<Task> taskAuthor;
        /// <summary>
        /// Где автор задачи
        /// </summary>
        public virtual IList<Task> TaskAuthor
        {
            get { return taskAuthor ?? (taskAuthor = new List<Task>()); }
            set { taskAuthor = value; }
        }

        private IList<SubTask> subtaskperformers;
        /// <summary>
        /// Где исполнитель задачи
        /// </summary>
        public virtual IList<SubTask> SubTaskPerformers
        {
            get { return subtaskperformers ?? (subtaskperformers = new List<SubTask>()); }
            set { subtaskperformers = value; }
        }

    }
}
