using FluentNHibernate.Mapping;
using Slobkoll.HRM.Core.Object;

namespace Slobkoll.HRM.Core.Mapping
{
    public class GroupMap : ClassMap<Group>
    {
        public GroupMap()
        {
            Id(x => x.Id);

            Map(x => x.Name)
                .Length(70)
                .Not.Nullable();

            HasManyToMany(x => x.User)
                .ParentKeyColumn("GroupID")
                .ChildKeyColumn("UserID")
                .Cascade.SaveUpdate()
                .Table("Group_User");

            HasManyToMany(x => x.UserPerformer)
                .ParentKeyColumn("GroupIDPerfomer")
                .ChildKeyColumn("UserID")
                .Cascade.SaveUpdate()
                .Table("User_GroupPerfomer");
        }
    }
}

