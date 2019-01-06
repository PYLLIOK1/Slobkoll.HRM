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

            Map(x => x.FileName)
                .Length(100)
                .Not.Nullable();

            Map(x => x.Files)
                .Length(int.MaxValue)
                .Not.Nullable();

            Map(x => x.DateBegin)
                .CustomSqlType("smalldatetime")
                .Not.Nullable();

            Map(x => x.DateEnd)
                .CustomSqlType("smalldatetime")
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
