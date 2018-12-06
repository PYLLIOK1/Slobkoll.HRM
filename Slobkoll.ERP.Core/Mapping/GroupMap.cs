using FluentNHibernate.Mapping;
using Slobkoll.ERP.Core.Object;

namespace Slobkoll.ERP.Core.Mapping
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
        }
    }
}

