using Newtonsoft.Json;
using RedoxMod.API.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace RedoxMod.Core.Configuration
{
    public class FileConfiguration : IConfiguration
    {
       
        public string FileName { get; }

        public string WorkingDirectory { get; }

        public string FullPath => Path.Combine(this.WorkingDirectory, this.FileName);

        public FileConfiguration(string fileName, string workingDirectory)
        {
            FileName = fileName;
            WorkingDirectory = workingDirectory;
        }

        public Task<bool> ExistsAsync()
        {      
            return Task.FromResult(File.Exists(this.FullPath));
        }    

        public async Task<object> LoadAsync()
        {
            return await this.LoadAsync<object>();
        }

        public async Task SaveAsync(object defaultConfig)
        {
            if (defaultConfig == null)
                throw new Exception("Failed to save config. Object is null!");

            if(!Directory.Exists(this.WorkingDirectory))
            {
                Directory.CreateDirectory(this.WorkingDirectory);
            }

            string json = JsonConvert.SerializeObject(defaultConfig, Formatting.Indented);
            await File.WriteAllTextAsync(this.FullPath, json);
        }

        public async Task<T> LoadAsync<T>()
        {
            bool exists = await this.ExistsAsync();

            if (!exists)
                return default;

            string json = await File.ReadAllTextAsync(this.FullPath);
            T ob = JsonConvert.DeserializeObject<T>(json);

            return ob;
        }
    }
}
