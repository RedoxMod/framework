using System;
using System.Reflection;
using NUnit.Framework;
using RedoxMod.Architecture;
using RedoxMod.Tests.Architecture.Concretes;
using RedoxMod.Tests.Architecture.Contracts;

namespace RedoxMod.Tests.Architecture
{
    public class ContainerTests
    {
        private Container _container;
        
        [SetUp]
        public void Setup()
        {
            this._container = new Container();
            
            this._container.Bind<ILogger, Logger>();
        }
        
        [Test]
        public void TestResolvingTheLoggerServiceFromTheContainer()
        {
            //Arrange
            ILogger logger = this._container.Resolve<ILogger>();
            
            //Act
            logger?.Log("Hello World!");
            
            //Assert
            Assert.IsTrue(logger != null);
        }
    }
}