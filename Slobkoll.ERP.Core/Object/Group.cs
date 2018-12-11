using System.Collections.Generic;

namespace Slobkoll.ERP.Core.Object
{
    public class Group
    {
        /// <summary>
        /// Ид группы
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// Название группы
        /// </summary>
        public virtual string Name { get; set; }

        private IList<User> user;
        /// <summary>
        /// Список группы
        /// </summary>
        public virtual IList<User> User
        {
            get { return user ?? (user = new List<User>()); }
            set { user = value; }
        }

        private IList<User> userPerformer;
        /// <summary>
        /// список групп которым может дать задачи
        /// </summary>
        public virtual IList<User> UserPerformer
        {
            get { return userPerformer ?? (userPerformer = new List<User>()); }
            set { userPerformer = value; }
        }

    }
}
