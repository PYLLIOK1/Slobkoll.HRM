﻿using FluentNHibernate.Mapping;
using Slobkoll.ERP.Core.Object;

namespace Slobkoll.ERP.Core.Mapping
{
    public class SubTaskMap : ClassMap<SubTask>
    {
        public SubTaskMap()
        {
            Id(x => x.Id);

            References(x => x.TaskId)
                .Cascade
                .SaveUpdate().
                Not.Nullable();

            Map(x => x.Name)
                .Length(70)
                .Not.Nullable();

            Map(x => x.Path)
                .Not.Nullable();

            Map(x => x.Status)
                .Not.Nullable();

            References(x => x.Performer)
                .Cascade
                .SaveUpdate().
                Not.Nullable();

            Map(x => x.ChangeAuthor)
                .Not.Nullable();

            Map(x => x.ChangePerformer)
                .Not.Nullable();
        }
    }
}

