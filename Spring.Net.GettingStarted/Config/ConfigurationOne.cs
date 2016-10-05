using NHibernate;
using NHibernate.Cfg;
using Spring.Aop.Framework.AutoProxy;
using Spring.Context.Attributes;
using Spring.Data.Common;
using Spring.Data.NHibernate;
using Spring.Net.GettingStarted.Dao;
using Spring.Net.GettingStarted.Model;
using Spring.Objects.Factory.Support;
using Spring.Transaction;
using Spring.Transaction.Interceptor;
using System.Collections.Generic;

namespace Spring.Net.GettingStarted.Config
{
    [Configuration]
    public class ConfigurationOne
    {
        [ObjectDef]
        [Scope(ObjectScope.Singleton)]
        public virtual IDbProvider DbProvider()
        {
            var provider = DbProviderFactory.GetDbProvider("System.Data.SqlClient");
            provider.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True";
            return provider;
        }

        [ObjectDef]
        [Scope(ObjectScope.Singleton)]
        public virtual LocalSessionFactoryObject LocalSessionFactoryObject()
        {
            return new SpringLocalSessionFactoryObject
            {
                DbProvider = DbProvider(),
                HibernateProperties = new Dictionary<string, string>
                {
                    {Environment.ConnectionProvider, "NHibernate.Connection.DriverConnectionProvider"},
                    {Environment.Dialect, "NHibernate.Dialect.MsSql2008Dialect"},
                    {Environment.ConnectionDriver, "NHibernate.Driver.SqlClientDriver" },
                    {Environment.ShowSql, "true" },
                    {Environment.Hbm2ddlAuto, "create" },
                    {Environment.Hbm2ddlKeyWords, "auto-quote" },
                },
                ExposeTransactionAwareSessionFactory = true
            };
        }

        [ObjectDef]
        [Scope(ObjectScope.Singleton)]
        public virtual IPlatformTransactionManager TransactionManager()
        {
            return new HibernateTransactionManager
            {
                DbProvider = DbProvider(),
                SessionFactory = SessionFactory()
            };
        }

        [ObjectDef]
        public virtual AttributeAutoProxyCreator TransactionProxyCreator()
        {
            return new AttributeAutoProxyCreator
            {
                Order = 3,
                AttributeTypes = new[] { typeof(TransactionAttribute) },
                InterceptorNames = new[] { nameof(ObjectFactoryTransactionAttributeSourceAdvisor) },
            };
        }

        [ObjectDef]
        [Scope(ObjectScope.Singleton)]
        public virtual AttributesTransactionAttributeSource AttributesTransactionAttributeSource()
        {
            return new AttributesTransactionAttributeSource();
        }


        [ObjectDef]
        [Scope(ObjectScope.Singleton)]
        public virtual TransactionInterceptor TransactionInterceptor()
        {
            return new TransactionInterceptor
            {
                TransactionAttributeSource = AttributesTransactionAttributeSource(),
                TransactionManager = TransactionManager()
            };
        }

        [ObjectDef]
        [Scope(ObjectScope.Singleton)]
        public virtual ObjectFactoryTransactionAttributeSourceAdvisor ObjectFactoryTransactionAttributeSourceAdvisor()
        {
            return new ObjectFactoryTransactionAttributeSourceAdvisor
            {
                TransactionAttributeSource = AttributesTransactionAttributeSource(),
                Advice = TransactionInterceptor(),
                Order = 1
            };
        }

        [ObjectDef]
        [Scope(ObjectScope.Prototype)]
        public virtual ISessionFactory SessionFactory()
        {
            return (ISessionFactory)LocalSessionFactoryObject();
        }


        [ObjectDef]
        [Scope(ObjectScope.Prototype)]
        public virtual IUserService UserService()
        {
            return new UserService();
        }

        [ObjectDef]
        [Scope(ObjectScope.Prototype)]
        public virtual IUserRepository UserRepository()
        {
            return new NHibernateUserRepository(SessionFactory());
        }
    }
}
