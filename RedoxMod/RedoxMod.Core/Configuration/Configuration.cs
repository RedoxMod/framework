using RedoxMod.API.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;

namespace RedoxMod.Core.Configuration
{
    public class Configuration : IConfiguration
    {
        public Configuration(string fileName, string workingDirectory)
        {
            FileName = fileName;
            WorkingDirectory = workingDirectory;
        }

        public string FileName { get; }

        public string WorkingDirectory { get; }

        public Task<bool> ExistsAsync(string fileName = "")
        {
            throw new NotImplementedException();
        }

        public Task<object> LoadConfigAsync(string fileName = "")
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveConfigAsync([DisallowNull] object defaultConfig, string fileName = "")
        {
            throw new NotImplementedException();
        }
    }
}
