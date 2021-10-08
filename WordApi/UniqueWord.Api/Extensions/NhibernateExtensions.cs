using Microsoft.Extensions.DependencyInjection;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;

namespace UniqueWord.Api.Extensions
{
    public static class NHibernateExtensions
    {
        public static IServiceCollection AddNHibernate(this IServiceCollection services, string connectionString)
        {
            var mapper = new ModelMapper();
            mapper.AddMappings(typeof(NHibernateExtensions).Assembly.ExportedTypes);
            HbmMapping hbmMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            var configuration = new Configuration();
            configuration.DataBaseIntegration(db =>
            {
                db.Dialect<MsSql2012Dialect>();
                db.ConnectionString = connectionString;
                db.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;

                db.LogFormattedSql = false;
                db.LogSqlInConsole = false;
                db.BatchSize = 100;
            });

            configuration.AddMapping(hbmMapping);

            var sessionFactory = configuration.BuildSessionFactory();
            services.AddSingleton(sessionFactory);
            services.AddScoped(f => sessionFactory.OpenSession());

            return services;
        }
    }
}
