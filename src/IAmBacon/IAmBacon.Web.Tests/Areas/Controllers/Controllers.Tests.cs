using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IAmBacon.Web.Tests.Areas.Controllers
{
    [TestClass]
    public class ControllersTests
    {
        [TestMethod]
        public void Should_Implement_Base_Controller()
        {
            // Arrange
            var binPath = System.AppDomain.CurrentDomain.BaseDirectory;
            var dll = Directory.GetFiles(binPath, "IAmBacon.dll", SearchOption.AllDirectories).First();

            // Act
            Assembly assembly = Assembly.LoadFile(dll);

            var types = assembly.GetTypes()
                .Where(x => 
                    x.Name.Contains("Controller") && 
                    x.Namespace == "IAmBacon.Areas.Admin.Controllers");

            // Assert
            foreach (var type in types)
            {
                if (type.IsAbstract) continue;
                if (type.BaseType == null) continue;

                Assert.IsTrue(type.BaseType.Name == "BaseController");
            }
        }
    }
}
