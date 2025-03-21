using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace RedoxMod.Core.Configuration
{
    [Serializable]
    public sealed class RedoxConfiguration
    {

        public GeneralSettings General { get; set; }

        public OptimizationSettings Optimization { get; set; }

        public RedoxConfiguration Init()
        {
            this.General = new GeneralSettings();
            this.Optimization = new OptimizationSettings();
            return this;
        }

        [Serializable]
        public sealed class GeneralSettings
        {
            [JsonProperty("development_mode - Only enable this when you're debugging your plugins")]
            public bool DevelopmentMode { get; set; } = false;

            [JsonProperty("enable_logging - Allow RedoxMod to log messages in the console")]
            public bool EnableLogging { get; set; } = true;
        }

        [Serializable]
        public sealed class OptimizationSettings
        {
            [JsonProperty("enable_optimization")]
            public bool EnableOptimization { get; set; } = true;

            [JsonProperty("disable_unnecessary_services - When enabled, RedoxMod will only inject explicitly required services into plugins.")]
            public bool DisableUnnecessaryServices { get; set; } = true;

            [JsonProperty("max_script_memory - Sets a memory usage cap for plugins (Default: 1 MB)")]
            public ulong MaxScriptMemory { get; set; } = 1_000_000;
        }

        [Serializable]
        public sealed class PluginManagementSettings
        {
            [JsonProperty("plugin_timeout_limit - The maximum execution time for a plugin before it starts giving warnings (in milliseconds).")]
            public uint PluginTimeoutLimit { get; set; } = 1000;

            [JsonProperty("force_stop_on_timout - Should a plugin be force-stopped when taking to long to load?")]
            public bool ForceStopOnTimeout { get; set; } = false;
        }
    }
}
