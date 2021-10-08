using NHibernate.Mapping.ByCode.Conformist;
using UniqueWord.Api.Mappings.Extensions;
using UniqueWord.Api.Models;

namespace UniqueWord.Api.Mappings
{
    public class TextEntryMapping : ClassMapping<TextEntry>
    {
        public TextEntryMapping()
        {
            this.AddIdMapping<TextEntryMapping, TextEntry>();

            Property(m => m.NoOfWords, m => m.NotNullable(true));

            Table("dbo.TextEntry");
        }
    }
}
