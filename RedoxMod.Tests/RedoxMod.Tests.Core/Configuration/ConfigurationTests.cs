using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Config = RedoxMod.Core.Configuration.FileConfiguration;

namespace RedoxMod.Tests.Core.Configuration
{
    [TestFixture]
    public class ConfigurationTests
    {
        private const string TestFileName = "config.json";
        private const string TestDirectory = "TestConfigs";
        private string _testFullPath;
        private Config _config;

        [SetUp]
        public void SetUp()
        {
            _testFullPath = Path.Combine(TestDirectory, TestFileName);
            _config = new Config(TestFileName, TestDirectory);
        }

        [Test]
        public async Task ExistsAsync_ShouldReturnFalse_WhenFileDoesNotExist()
        {
            // Act
            bool exists = await _config.ExistsAsync();

            // Assert
            Assert.IsFalse(exists);
        }

        [Test]
        public async Task ExistsAsync_ShouldReturnTrue_WhenFileExists()
        {
            // Arrange
            Directory.CreateDirectory(TestDirectory);
            File.WriteAllText(_testFullPath, "{}");

            // Act
            bool exists = await _config.ExistsAsync();

            // Assert
            Assert.IsTrue(exists);

            // Cleaning
            File.Delete(_testFullPath);
        }

        [Test]
        public async Task LoadConfigAsync_ShouldReturnNull_WhenFileDoesNotExist()
        {
            // Act
            var result = await _config.LoadAsync();

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public Task SaveConfigAsync_ShouldThrowException_WhenObjectIsNull()
        {
            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () => await _config.SaveAsync(null));
            Assert.AreEqual("Failed to save config. Object is null!", ex.Message);
            return Task.CompletedTask;
        }
    }
}
