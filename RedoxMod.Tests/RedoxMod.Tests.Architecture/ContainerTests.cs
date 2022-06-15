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
            this._container.Instance<IApplication>(new Application());
            IApplication app = this._container.Resolve<IApplication>();
            
            //Act
            
            Console.WriteLine(app?.Version);

            //Assert

            Assert.NotNull(app);

        }
    }
}