using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using UniqueWord.Api.Mappings.Extensions;
using UniqueWord.Api.Models;

namespace UniqueWord.Api.Mappings
{
    public class UniqueWordEntryMapping : ClassMapping<UniqueWordEntry>
    {
        public UniqueWordEntryMapping()
        {
            this.AddIdMapping<UniqueWordEntryMapping, UniqueWordEntry>();
            Property(m => m.Word, m => m.NotNullable(true));
            ManyToOne(m => m.TextEntry, m =>
            {
                m.Access(Accessor.Field);
                m.Column("TextEntryId");
                m.NotNullable(true);
                m.Cascade(Cascade.Persist);
            });

            Table("dbo.UniqueWordEntry");
        }
    }
}