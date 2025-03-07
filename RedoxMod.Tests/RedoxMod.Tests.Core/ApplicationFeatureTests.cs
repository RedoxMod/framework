using NUnit.Framework;
using RedoxMod.API;
using RedoxMod.Core;
using System.IO;
using System.Threading.Tasks;
using static RedoxMod.Tests.Core.ServiceProviderTests;

namespace RedoxMod.Tests.Core
{
    public class ApplicationFeatureTests
    {
        [Test]
        public async Task Application_Should_Initialize_And_Register_Itself()
        {
            // Arrange
            string testBasePath = Path.Combine(Path.GetTempPath(), "redox_feature_test");
            var app = new RedoxApplication(testBasePath);

            // Act
            await app.InitializeAsync();

            // Assert
            Assert.That(Directory.Exists(app.PluginsPath), Is.True);
            Assert.That(Directory.Exists(app.LangPath), Is.True);
            Assert.That(Directory.Exists(app.ConfigurationsPath), Is.True);
            Assert.That(app.Container.Resolve<IRedoxApplication>(), Is.SameAs(app));

            // Cleanup
            Directory.Delete(testBasePath, true);
        }

        [Test]
        public async Task Application_Should_Register_And_Resolve_ServiceProviders()
        {
            // Arrange
            var app = new RedoxApplication();
            await app.InitializeAsync();

            // Act
            app.RegisterServiceProvider<TestServiceProvider>();

            // Assert
            Assert.That(app.Providers.Length, Is.EqualTo(1));
            Assert.That(app.Providers[0], Is.InstanceOf<TestServiceProvider>());
        }

        [Test]
        public async Task Application_Full_Lifecycle_Should_Work_Correctly()
        {
            // Arrange
            var app = new RedoxApplication();
            await app.InitializeAsync();
            app.RegisterServiceProvider<TestServiceProvider>();

            // Act
            await app.LoadServiceAsync();
            await app.UnloadServiceAsync();

            // Assert
            Assert.Pass(); // No crashes mean the full lifecycle worked
        }
    }
}
