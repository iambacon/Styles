using System;
using System.Web.Mvc;
using IAmBacon.Areas.Admin.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IAmBacon.Web.Tests.Areas.Controllers
{
    [TestClass]
    public class BaseControllerTests
    {
        [TestMethod]
        public void Should_Have_Authorize_Attribute()
        {
            // Arrange

            // Act
            var attribute = Attribute.GetCustomAttribute(typeof(HomeController),
                typeof(AuthorizeAttribute));

            // Assert
            Assert.IsNotNull(attribute);
        }
    }
}
