using Microsoft.AspNetCore.Mvc;
using SecretSanta.Controllers;
using System;
using Xunit;

namespace SecretSanta.Tests.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public void Index()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = (ViewResult)controller.Index();

            var mvcName = typeof(Controller).Assembly.GetName();
            var isMono = Type.GetType("Mono.Runtime") != null;

            var expectedVersion = mvcName.Version.Major + "." + mvcName.Version.Minor;
            var expectedRuntime = isMono ? "Mono" : ".NET";

            // Assert
            Assert.Equal(expectedVersion, result.ViewData["Version"]);
            Assert.Equal(expectedRuntime, result.ViewData["Runtime"]);
        }
    }
}
