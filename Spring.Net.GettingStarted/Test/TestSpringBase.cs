using NUnit.Framework;
using Spring.Context;
using Spring.Context.Support;
using System.Linq;

namespace Spring.Net.GettingStarted
{
    [TestFixture]
    public abstract class TestSpringBase<TConfiguration>
    {
        protected IApplicationContext ApplicationContext;

        [SetUp]
        public virtual void SetUp()
        {
            CreateAndSetApplicationContext();
            AutoWire(this);
        }

        protected virtual void CreateAndSetApplicationContext()
        {
            var codeConfigApplicationContext = new CodeConfigApplicationContext();
            codeConfigApplicationContext.Scan(assembly =>
            {
                return typeof(TConfiguration).Assembly == assembly;
            }, type =>
            {
                return typeof(TConfiguration) == type;
            });
            codeConfigApplicationContext.Refresh();
            ApplicationContext = codeConfigApplicationContext;
        }

        protected virtual void AutoWire(object target)
        {
            foreach (var property in target.GetType().GetProperties())
            {
                var dic = ApplicationContext.GetObjectsOfType(property.PropertyType);
                if (dic.Count == 1)
                {
                    property.SetValue(target, dic.Values.First());
                }
            }
        }
    }
}
