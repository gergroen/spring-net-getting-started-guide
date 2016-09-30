using NUnit.Framework;

namespace Spring.Net.GettingStarted
{
    [TestFixture]
    public class TestSpringConfigurationOne : TestSpringBase<ConfigurationOne>
    {
        [Test]
        public void Test()
        {
            var userService = ApplicationContext.GetObject<IUserService>();
            Assert.IsNotNull(userService);

            //Container Scope prototype and singleton
            //Aop logging, caching and nhibernate
            //Validation error resource files

        }
    }
}
