using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using RedoxMod.Core;
using RedoxMod.Core.Configuration;

namespace RedoxMod.Tests.Core
{
    [TestFixture]
    public class RedoxConfigurationTests
    {
        private string _testConfigPath;
        private RedoxApplication _app;

        [SetUp]
        public async Task Setup()
        {
            _testConfigPath = Path.Combine(Path.GetTempPath(), "RedoxTestConfig");
            _app = new RedoxApplication(_testConfigPath);
            await _app.InitializeAsync();
        }

        [TearDown]
        public void Cleanup()
        {
            if (Directory.Exists(_testConfigPath))
            {
                Directory.Delete(_testConfigPath, true);
            }
        }

        [Test]
        public void Config_File_Should_Be_Created()
        {
            string configFile = Path.Combine(_testConfigPath, "redox_config.json");
            Assert.That(File.Exists(configFile), Is.True, "Configuration file was not created.");
        }

        [Test]
        public void Config_Should_Have_Default_Values()
        {
            Assert.That(_app.Config, Is.Not.Null, "Config object is null.");
            Assert.That(_app.Config.General.DevelopmentMode, Is.False, "DevelopmentMode should default to false.");
            Assert.That(_app.Config.General.EnableLogging, Is.True, "EnableLogging should default to true.");
            Assert.That(_app.Config.Optimization.EnableOptimization, Is.True, "EnableOptimization should default to true.");
            Assert.That(_app.Config.Optimization.DisableUnnecessaryServices, Is.True, "DisableUnnecessaryServices should default to true.");
            Assert.That(_app.Config.Optimization.MaxScriptMemory, Is.EqualTo(1_000_000), "MaxScriptMemory should default to 1MB.");
            Assert.That(_app.Config.PluginManagement.PluginTimeoutLimit, Is.EqualTo(1000));
            Assert.That(_app.Config.PluginManagement.ForceStopOnTimeout, Is.False);
        }

        [Test]
        public async Task Config_Should_Load_From_File()
        {
            string configFile = Path.Combine(_testConfigPath, "redox_config.json");
            var newConfig = new RedoxConfiguration
            {
                General = new RedoxConfiguration.GeneralSettings { DevelopmentMode = true, EnableLogging = false },
                Optimization = new RedoxConfiguration.OptimizationSettings { EnableOptimization = false, MaxScriptMemory = 2_000_000 },
                PluginManagement = new RedoxConfiguration.PluginManagementSettings { ForceStopOnTimeout = true, PluginTimeoutLimit = 2000 }
            };

            await File.WriteAllTextAsync(configFile, JsonConvert.SerializeObject(newConfig, Formatting.Indented));

            _app = new RedoxApplication(_testConfigPath);
            await _app.InitializeAsync();

            Assert.That(_app.Config.General.DevelopmentMode, Is.True);
            Assert.That(_app.Config.General.EnableLogging, Is.False);
            Assert.That(_app.Config.Optimization.EnableOptimization, Is.False);
            Assert.That(_app.Config.Optimization.MaxScriptMemory, Is.EqualTo(2_000_000));
            Assert.That(_app.Config.PluginManagement.PluginTimeoutLimit, Is.EqualTo(2000));
            Assert.That(_app.Config.PluginManagement.ForceStopOnTimeout, Is.True);
        }
    }
}
