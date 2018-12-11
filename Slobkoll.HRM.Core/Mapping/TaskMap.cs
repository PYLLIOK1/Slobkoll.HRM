using FluentNHibernate.Mapping;
using Slobkoll.HRM.Core.Object;



namespace Slobkoll.HRM.Core.Mapping
{
    public class TaskMap : ClassMap<Task>
    {
        public TaskMap()
        {
            Id(x => x.Id);

            Map(x => x.Name)
                .Length(70)
                .Not.Nullable();

            Map(x => x.Description)
                .Length(300)
                .Not.Nullable();

            Map(x => x.Path)
                .Not.Nullable();

            Map(x => x.DateBegin)
                .Not.Nullable();

            Map(x => x.DateEnd)
                .Not.Nullable();

            Map(x => x.Status)
                .Not.Nullable();

            References(x => x.Author)
                .Cascade
                .SaveUpdate()
                .Not.Nullable();

            Map(x => x.Change)
                .Not.Nullable();

            HasMany(x => x.SubTask)
                .Inverse();
        }
    }
}
