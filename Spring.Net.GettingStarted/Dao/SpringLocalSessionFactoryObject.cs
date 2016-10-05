using Spring.Data.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring.Net.GettingStarted.Dao
{
    public class SpringLocalSessionFactoryObject : LocalSessionFactoryObject
    {
        protected override void PostProcessMappings(Configuration config)
        {
            var mapper = new ModelMapper();
            mapper.AddMappings(new List<System.Type> { typeof(UserMap) });

            var hbmMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            config.AddDeserializedMapping(hbmMapping, null);
        }
    }
}
