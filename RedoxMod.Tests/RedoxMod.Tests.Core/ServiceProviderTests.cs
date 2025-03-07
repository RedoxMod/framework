using NUnit.Framework;
using RedoxMod.API;
using RedoxMod.Core;

namespace RedoxMod.Tests.Core
{
    public class ServiceProviderTests
    {

        public class TestServiceProvider : ServiceProvider
        {
            public TestServiceProvider(IRedoxApplication app) : base(app) { }
            public override void Register() { }
        }


        [Test]
        public void RegisterServiceProvider_Should_Add_New_ServiceProvider()
        {
            // Arrange
            var app = new RedoxApplication();

            // Act
            app.RegisterServiceProvider<TestServiceProvider>();

            // Assert
            Assert.That(app.Providers.Length, Is.EqualTo(1));
            Assert.That(app.Providers[0], Is.InstanceOf<TestServiceProvider>());
        }

        [Test]
        public void RegisterServiceProvider_Should_Not_Add_Duplicate_Providers()
        {
            // Arrange
            var app = new RedoxApplication();

            // Act
            app.RegisterServiceProvider<TestServiceProvider>();
            app.RegisterServiceProvider<TestServiceProvider>();  // Register again

            // Assert
            Assert.That(app.Providers.Length, Is.EqualTo(1));  // Should still be only one
        }

     
    }
}
