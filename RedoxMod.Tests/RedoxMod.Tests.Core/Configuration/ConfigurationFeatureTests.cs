using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Config = RedoxMod.Core.Configuration.Configuration;

namespace RedoxMod.Tests.Core.Configuration
{
    [TestFixture]
    public class ConfigurationFeatureTests
    {
        private const string TestFileName = "feature_config.json";
        private const string TestDirectory = "FeatureConfigs";
        private string _testFullPath;
        private Config _config;

        [SetUp]
        public void SetUp()
        {
            _testFullPath = Path.Combine(TestDirectory, TestFileName);
            _config = new Config(TestFileName, TestDirectory);
        }

        [Test]
        public async Task SaveConfigAsync_ShouldCreateFileWithCorrectJson()
        {
            // Arrange
            var testData = new { Name = "RedoxMod", Version = "1.0" };

            // Act
            await _config.SaveConfigAsync(testData);

            // Assert
            Assert.IsTrue(File.Exists(_testFullPath));

            string fileContent = File.ReadAllText(_testFullPath);
            dynamic deserialized = JsonConvert.DeserializeObject<dynamic>(fileContent);

            Assert.AreEqual("RedoxMod", (string)deserialized.Name);
            Assert.AreEqual("1.0", (string)deserialized.Version);
        }

        [Test]
        public async Task LoadConfigAsync_ShouldReturnDeserializedObject()
        {
            // Arrange
            var testData = new { Name = "RedoxMod", Version = "1.0" };
            string json = JsonConvert.SerializeObject(testData, Formatting.Indented);

            Directory.CreateDirectory(TestDirectory);
            File.WriteAllText(_testFullPath, json);

            // Act
            var result = await _config.LoadConfigAsync();

            // Assert
            Assert.IsNotNull(result);
            dynamic deserialized = JsonConvert.DeserializeObject<dynamic>(JsonConvert.SerializeObject(result));

            Assert.AreEqual("RedoxMod", (string)deserialized.Name);
            Assert.AreEqual("1.0", (string)deserialized.Version);
        }
    }
}
