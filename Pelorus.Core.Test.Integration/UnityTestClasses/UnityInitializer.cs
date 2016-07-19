using Microsoft.Practices.Unity;
using Pelorus.Unity;

namespace Pelorus.Core.Test.Integration.UnityTestClasses
{
    public class UnityInitializer : BaseUnityInitializer
    {
        public UnityInitializer(IUnityContainer container) : base(container)
        {
        }

        protected override void ConfigureMappers()
        {
            this.AddMapper<TestMapper>();
        }
    }
}
