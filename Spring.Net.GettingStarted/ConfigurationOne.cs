using Spring.Context.Attributes;
using Spring.Objects.Factory.Support;

namespace Spring.Net.GettingStarted
{
    [Configuration]
    public class ConfigurationOne
    {
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
            return new NHibernateUserRepository();
        }
    }

    public class User
    {
        public virtual string Name { get; set; }
    }

    public interface IUserService
    {
    }

    public class UserService : IUserService
    {
    }

    public interface IUserRepository
    {
    }

    public class NHibernateUserRepository : IUserRepository
    {

    }
}
