using FluentNHibernate.Mapping;
using Slobkoll.HRM.Core.Object;

namespace Slobkoll.HRM.Core.Mapping
{
    public class CommentMap : ClassMap<Comment>
    {
        public CommentMap()
        {
            Id(x => x.Id);

            Map(x => x.TextComment)
                .Length(300)
                .Not.Nullable();

            References(x => x.Author)
                .Cascade
                .SaveUpdate().
                Not.Nullable();

            References(x => x.SubTask)
                .Cascade
                .SaveUpdate().
                Not.Nullable();
        }
    }
}
