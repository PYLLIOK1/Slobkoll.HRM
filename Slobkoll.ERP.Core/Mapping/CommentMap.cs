using FluentNHibernate.Mapping;
using Slobkoll.ERP.Core.Object;

namespace Slobkoll.ERP.Core.Mapping
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
