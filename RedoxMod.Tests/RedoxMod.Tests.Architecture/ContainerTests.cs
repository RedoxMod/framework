using System;
using System.Collections.Generic;
using NUnit.Framework;
using RedoxMod.API;
using RedoxMod.Architecture;
using RedoxMod.Core;
using RedoxMod.Tests.Architecture.Concretes;
using RedoxMod.Tests.Architecture.Contracts;

namespace RedoxMod.Tests.Architecture
{
    [TestFixture]
    public class ContainerTests
    {
        private Container _container;
        
        [SetUp]
        public void Setup()
        {
            this._container = new Container();
        }
        
        [Test]
        public void TestResolvingTheLoggerServiceFromTheContainer()
        {
            //Arrange
            this._container.Bind<ILogger, Logger>();
            ILogger logger = this._container.Resolve<ILogger>();
            
            //Act
            logger?.Log("Hello World!");
            
            //Assert
            Assert.NotNull(logger);
        }

        [Test]
        public void Resolve_And_Test_The_MessageProvider_To_See_If_Constructor_Injection_Works_Or_Not()
        {
            //Arrange
            this._container.Bind<ILogger, Logger>();
            this._container.Bind<IMessageProvider, MessageProvider>();
            
            IMessageProvider messageProvider = this._container.Resolve<IMessageProvider>();
            
            //Act
            messageProvider?.Greet();
            messageProvider?.Goodbye();
            
            //Assert
            Assert.NotNull(messageProvider);
        }

        [Test]
        public void Bind_And_Resolve_An_Existing_Instance_Dependency()
        {
            //Arrange
            this._container.Instance<IRedoxApplication>(new RedoxApplication());
            IRedoxApplication app = this._container.Resolve<IRedoxApplication>();
            
            //Act
            
            Console.WriteLine(app?.Version);

            //Assert

            Assert.NotNull(app);

        }
    }
}