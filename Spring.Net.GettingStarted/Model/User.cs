using System;

namespace Spring.Net.GettingStarted.Model
{
    public class User
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Language { get; set; }
    }
}
