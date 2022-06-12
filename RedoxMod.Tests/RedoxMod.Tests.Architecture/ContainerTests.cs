
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
            this._container.Bind<IMessageProvider, MessageProvider>();
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
            IMessageProvider messageProvider = this._container.Resolve<IMessageProvider>();
            
            //Act
            messageProvider?.Greet();
            messageProvider?.Goodbye();
            
            //Assert
            Assert.NotNull(messageProvider);
        }
    }
}