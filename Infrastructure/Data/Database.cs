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
        public static ISessionFactory CreateSessionFactory(string connectionString)
        {
            return Fluently.Configure()
                .Database(
                    MySQLConfiguration.Standard
                        .ConnectionString(connectionString))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ShortUrlMap>())
                .ExposeConfiguration(UpdateSchema)
                .BuildSessionFactory();
        }

        private static void UpdateSchema(Configuration config)
        {
            new SchemaUpdate(config).Execute(false, true);
        }
    }
}
