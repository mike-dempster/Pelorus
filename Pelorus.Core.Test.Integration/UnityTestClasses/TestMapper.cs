using Pelorus.Core.IoC;

namespace Pelorus.Core.Test.Integration.UnityTestClasses
{
    public class TestMapper : BaseMapper
    {
        public override void MapContracts()
        {
            this.Map<ITest, Test0>();
        }
    }
}
