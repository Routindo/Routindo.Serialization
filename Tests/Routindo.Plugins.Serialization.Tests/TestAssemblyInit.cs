using Microsoft.VisualStudio.TestTools.UnitTesting;
using Routindo.Contract.Services;
using Routindo.Plugins.Serialization.Tests.Mock;

namespace Routindo.Plugins.Serialization.Tests
{
    [TestClass]
    public class TestAssemblyInit 
    {
        [AssemblyInitialize]
        public static void Initialize(TestContext testContext)
        { 
            ServicesContainer.SetServicesProvider(new FakeServicesProvider());
        }
    }
}
