using System;

namespace Spring.Net.GettingStarted.Model
{
    public interface IUserRepository
    {
        User Get(Guid id);
        void Save(User user);
    }
}
