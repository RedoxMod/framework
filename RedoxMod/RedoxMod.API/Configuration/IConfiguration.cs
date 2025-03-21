using System;
using System.Collections.Generic;
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

        string FullPath { get; }

        Task<T> LoadAsync<T>();
        Task<object> LoadAsync();

        Task SaveAsync(object defaultConfig);

        Task<bool> ExistsAsync();
    }
}
