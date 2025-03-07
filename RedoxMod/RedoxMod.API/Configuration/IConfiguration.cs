using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;

namespace RedoxMod.API.Configuration
{
    /// <summary>
    /// Represents the contract for every Configuration.
    /// </summary>
    public interface IConfiguration
    {
        string FileName { get; }

        string WorkingDirectory { get; }

        Task<object> LoadConfigAsync(string fileName = "");

        Task<bool> SaveConfigAsync([DisallowNull]object defaultConfig, string fileName = "");

        Task<bool> ExistsAsync(string fileName = "");
    }
}
