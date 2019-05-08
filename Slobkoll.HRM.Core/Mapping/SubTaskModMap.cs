using FluentNHibernate.Mapping;
using Slobkoll.HRM.Core.Object;

namespace Slobkoll.HRM.Core.Mapping
{
    public class SubTaskModMap : ClassMap<SubTaskMod>
    {
        public SubTaskModMap()
        {
            Id(x => x.Id);

            Map(x => x.Status)
                .Length(300)
                .Not.Nullable();

            Map(x => x.DateTime)
                .CustomSqlType("smalldatetime")
                .Not.Nullable();

            References(x => x.SubTaskId)
                .Cascade
                .SaveUpdate()
                .Not.Nullable();
        }
    }
}
