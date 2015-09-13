using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pelorus.Core.Configuration;
using Pelorus.Core.Test.Integration.UnityTestClasses;

namespace Pelorus.Core.Test.Integration
{
    [TestClass]
    public class UnityTests
    {
        [TestMethod]
        public void TestConfig()
        {
            var config = CoreConfiguration.IoC;
            Assert.IsNotNull(config);
        }

        [TestMethod]
        public void TestUnity()
        {
            var container = new UnityContainer();
            var init = new UnityInitializer(container);
            init.InitializeContainer();
            var tst = container.Resolve<ITest>();
            tst.Hello();
        }
    }
}
