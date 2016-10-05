using NHibernate;
using Spring.Net.GettingStarted.Model;
using Spring.Transaction.Interceptor;
using System;

namespace Spring.Net.GettingStarted.Dao
{
    [Transaction]
    public class NHibernateUserRepository : IUserRepository
    {
        private readonly ISessionFactory _sessionFactory;

        public NHibernateUserRepository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public ISession Session
        {
            get { return _sessionFactory.GetCurrentSession(); }
        }

        [Transaction]
        public void Save(User user)
        {
            Session.Save(user);

        }

        [Transaction(ReadOnly = true)]
        public User Get(Guid id)
        {
            return Session.Get<User>(id);
        }
    }
}
