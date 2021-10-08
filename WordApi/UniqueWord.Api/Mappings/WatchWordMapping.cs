using NHibernate.Mapping.ByCode.Conformist;
using UniqueWord.Api.Mappings.Extensions;
using UniqueWord.Api.Models;

namespace UniqueWord.Api.Mappings
{
    public class WatchWordMapping : ClassMapping<WatchWord>
    {
        public WatchWordMapping()
        {
            this.AddIdMapping<WatchWordMapping, WatchWord>();
            Property(m => m.Word, m => m.NotNullable(true));

            Table("dbo.WatchListWord");
        }
    }
}
