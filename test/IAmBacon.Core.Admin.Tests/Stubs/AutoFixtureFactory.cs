using System.Linq;
using Ploeh.AutoFixture;

namespace IAmBacon.Core.Admin.Tests.Stubs
{
    public static class AutoFixtureFactory
    {
        public static Fixture CreateOmitOnRecursionFixture()
        {
            //from https://github.com/AutoFixture/AutoFixture/issues/337
            var fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior()); //recursionDepth

            return fixture;
        }
    }
}
