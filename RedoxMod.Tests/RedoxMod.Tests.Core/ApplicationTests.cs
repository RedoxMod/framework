using System;
using NUnit.Framework;
using RedoxMod.API;
using RedoxMod.Core;

namespace RedoxMod.Tests.Core
{
    [TestFixture]
    public class ApplicationTests
    {
        private IApplication _app;
        
        [SetUp]
        public void Setup()
        {
            this._app = new Application();
        }

        [Test]
        public void Test_The_Initialization_Of_The_Application()
        {
            //Arrange
            
            //Act
            
            //Assert
        }
    }
}