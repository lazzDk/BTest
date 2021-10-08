using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using UniqueWord.Api.Models.Interfaces;

namespace UniqueWord.Api.Mappings.Extensions
{
    internal static class MappingExtensions
    {
        internal static void AddIdMapping<TMapping, TType>(this TMapping mapping)
            where TMapping : ClassMapping<TType>
            where TType : class, IHasId
        {
            mapping.Id(m => m.Id, m =>
            {
                m.Generator(Generators.HighLow, g => g.Params(new { max_lo = 100 }));
                m.Column("Id");
            });
        }
    }
}