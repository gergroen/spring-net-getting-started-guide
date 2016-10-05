using NUnit.Framework;
using Spring.Net.GettingStarted.Config;
using Spring.Net.GettingStarted.Model;

namespace Spring.Net.GettingStarted
{
    [TestFixture]
    public class TestSpringConfigurationOne : TestSpringBase<ConfigurationOne>
    {
        public IUserService UserService { get; set; }

        public IUserRepository UserRepository { get; set; }

        [Test]
        public void Test()
        {
            Assert.IsNotNull(UserService);

            var user = new User {
                Name = "NAME",
                Language = "nl"
            };

            UserRepository.Save(user);

            user = UserRepository.Get(user.Id);
            Assert.AreEqual("NAME", user.Name);
            Assert.AreEqual("nl", user.Language);
            //Container Scope prototype and singleton
            //Aop logging, caching and nhibernate
            //Validation error resource files

        }
    }
}
