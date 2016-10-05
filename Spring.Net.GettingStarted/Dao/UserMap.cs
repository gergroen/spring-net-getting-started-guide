using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using Spring.Net.GettingStarted.Model;

namespace Spring.Net.GettingStarted.Dao
{
    public class UserMap : ClassMapping<User>
    {
        public UserMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.GuidComb));
            Property(x => x.Name);
            Property(x => x.Language);
        }
    }
}
