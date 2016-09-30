using NUnit.Framework;
using Spring.Context;
using Spring.Context.Support;

namespace Spring.Net.GettingStarted
{
    [TestFixture]
    public abstract class TestSpringBase<TConfiguration>
    {
        protected IApplicationContext ApplicationContext;

        [SetUp]
        public void SetUp()
        {
            var codeConfigApplicationContext = new CodeConfigApplicationContext();
            codeConfigApplicationContext.Scan(a =>
            {
                return typeof(TConfiguration).Assembly == a;
            }, t =>
            {
                return typeof(TConfiguration) == t;
            });
            codeConfigApplicationContext.Refresh();
            ApplicationContext = codeConfigApplicationContext;
        }
    }
}
