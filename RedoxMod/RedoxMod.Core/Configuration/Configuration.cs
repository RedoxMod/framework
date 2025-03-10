using Newtonsoft.Json;
using RedoxMod.API.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RedoxMod.Core.Configuration
{
    public class Configuration : IConfiguration
    {
       
        public string FileName { get; }

        public string WorkingDirectory { get; }

        public string FullPath => Path.Combine(this.WorkingDirectory, this.FileName);

        public Configuration(string fileName, string workingDirectory)
        {
            FileName = fileName;
            WorkingDirectory = workingDirectory;
        }

        public Task<bool> ExistsAsync()
        {      
            return Task.FromResult(File.Exists(this.FullPath));
        }    

        public async Task<object> LoadConfigAsync()
        {

            bool exists = await this.ExistsAsync();

            if (!exists) 
                return null;

            string json = await File.ReadAllTextAsync(this.FullPath);
            object ob = JsonConvert.DeserializeObject(json);

            return ob;
        }

        public async Task SaveConfigAsync(object defaultConfig)
        {
            if (defaultConfig == null)
                throw new Exception("Failed to save config. Object is null!");

            string json = JsonConvert.SerializeObject(defaultConfig, Formatting.Indented);
            await File.WriteAllTextAsync(this.FullPath, json);
        }      
    }
}
