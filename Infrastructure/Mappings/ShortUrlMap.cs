using Domain.Entities;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mappings
{
    public class ShortUrlMap : ClassMap<ShortUrl>
    {
        public ShortUrlMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.LongUrl);
            Map(x => x.ShortUrlId);
            Map(x => x.ClickCount);
            Map(x => x.CreationTime);
        }
    }
}
