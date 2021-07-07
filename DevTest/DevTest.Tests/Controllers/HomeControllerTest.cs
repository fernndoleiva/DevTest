using DevTest;
using DevTest.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace DevTest.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        

        [TestMethod]
        public void ExpotarXML()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            FileContentResult result = (FileContentResult)controller.ExpotarXML();

            // Assert
            Assert.IsNotNull(result);
        }


          
    }
}
