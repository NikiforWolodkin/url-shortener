using Domain.Entities;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernate;
using NHibernate.Cfg;
using Infrastructure.Mappings;

namespace Infrastructure.Data
{
    public static class Database
    {
        public static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(
                    MySQLConfiguration.Standard
                        .ConnectionString(c => c
                            .Server("localhost")
                            .Database("urlshortenerdb")
                            .Username("root")
                            .Password("SYSTEM")))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ShortUrlMap>())
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }

        private static void BuildSchema(Configuration config)
        {
            new SchemaExport(config).Create(false, true);
        }
    }
}
