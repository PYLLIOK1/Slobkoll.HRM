using FluentNHibernate.Mapping;
using Slobkoll.HRM.Core.Object;

namespace Slobkoll.HRM.Core.Mapping
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("Users");
            Id(x => x.Id);

            Map(x => x.Login)
                .Length(30)
                .Not.Nullable();

            Map(x => x.Password)
                .Length(50)
                .Not.Nullable();

            Map(x => x.Name)
                .Length(70).
                Not.Nullable();

            Map(x => x.Position)
                .Length(50)
                .Not.Nullable();

            Map(x => x.AdminRole).
                Not.Nullable();

            Map(x => x.StatusUser)
                .Not.Nullable();

            HasManyToMany(x => x.Group)
                .Cascade.SaveUpdate()
                .Inverse().Table("Group_User")
                .ParentKeyColumn("UserID")
                .ChildKeyColumn("GroupID");

            HasManyToMany(x => x.GroupPerformer)
                .Cascade.SaveUpdate()
                .Inverse().Table("User_GroupPerfomer")
                .ParentKeyColumn("UserID")
                .ChildKeyColumn("GroupIDPerfomer");

            HasManyToMany(x => x.UserObserved)
                .Cascade.SaveUpdate()
                .ParentKeyColumn("UserObservedid")
                .ChildKeyColumn("UserObserverid")
                .Table("UserObserved_UserObserver");

            HasManyToMany(x => x.UserObserver)
                .Cascade.SaveUpdate()
                .ParentKeyColumn("UserObserverid")
                .ChildKeyColumn("UserObservedid")
                .Table("UserObserved_UserObserver");

            HasManyToMany(x => x.UserCustomer)
                .Cascade.SaveUpdate()
                .ParentKeyColumn("UserPerformerid")
                .ChildKeyColumn("UserCustomerid")
                .Table("UserPerformer_UserCustomer");

            HasManyToMany(x => x.UserPerformer)
                .Cascade.SaveUpdate()
                .ParentKeyColumn("UserCustomerid")
                .ChildKeyColumn("UserPerformerid")
                .Table("UserPerformer_UserCustomer");
        }
    }
}