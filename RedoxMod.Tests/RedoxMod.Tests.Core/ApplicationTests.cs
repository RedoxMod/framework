using System;
using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;
using RedoxMod.API;
using RedoxMod.Architecture;
using RedoxMod.Core;

namespace RedoxMod.Tests.Core
{
    [TestFixture]
    public class ApplicationTests
    {
        private IRedoxApplication _app;
        
        [SetUp]
        public void Setup()
        {
            this._app = new RedoxApplication();
        }

        #region Unit test the basepath.

        [Test]
        public void Constructor_Should_Set_BasePath_To_Default_If_Empty()
        {
            // Act
            var app = new RedoxApplication();

            // Assert
            Assert.That(app.BasePath, Is.EqualTo(Path.Combine(Directory.GetCurrentDirectory(), "redox")));
        }

        [Test]
        public void Constructor_Should_Set_BasePath_When_Provided()
        {
            // Arrange
            string customPath = "/custom/path";

            // Act
            var app = new RedoxApplication(customPath);

            // Assert
            Assert.That(app.BasePath, Is.EqualTo(customPath));
        }

        #endregion

        [Test]
        public async Task InitializeAsync_Should_Create_Necessary_Directories()
        {
            // Arrange
            string testBasePath = Path.Combine(Path.GetTempPath(), "redox_test");
            var app = new RedoxApplication(testBasePath);

            // Act
            await app.InitializeAsync();

            // Assert
            Assert.That(Directory.Exists(app.BasePath), Is.True);
            Assert.That(Directory.Exists(app.PluginsPath), Is.True);
            Assert.That(Directory.Exists(app.LangPath), Is.True);
            Assert.That(Directory.Exists(app.ConfigurationsPath), Is.True);

            // Cleanup
            Directory.Delete(testBasePath, true);
        }

        [Test]
        public async Task InitializeAsync_Should_Initialize_Container()
        {
            // Arrange
            var app = new RedoxApplication();

            // Act
            await app.InitializeAsync();

            // Assert
            Assert.That(app.Container, Is.Not.Null);
        }

        [Test]
        public async Task InitializeAsync_Should_Register_Application_In_Container()
        {
            // Arrange
            var app = new RedoxApplication();

            // Act
            await app.InitializeAsync();
            var resolvedApp = app.Container.Resolve<IRedoxApplication>();

            // Assert

            Assert.That(resolvedApp, Is.SameAs(app));
        }

        [Test]
        public void Version_Should_Return_Correct_Semantic_Version()
        {
            // Arrange
            var app = new RedoxApplication();

            // Act
            var version = app.Version;

            // Assert
            Assert.That(version, Is.Not.Null);
            Assert.That(version.Major, Is.GreaterThanOrEqualTo(0)); // Version must be valid
        }
    }
}