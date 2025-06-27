using System.Reflection;
using System.Text.Json;

namespace HttpWebServer.CL.Models.Configuration
{
    public class ConfigurationHelper
    {
        private static readonly Lazy<ConfigurationHelper> configurationHelper = new Lazy<ConfigurationHelper>(() => new ConfigurationHelper());
        private ConfigurationHelper(){}

        public static ConfigurationHelper Instance
        {
            get { return configurationHelper.Value; }
        }

        public Configuration LoadConfigurationFile()
        {
            try
            {
                string assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string projectOutputDirectory = Path.GetFullPath(Path.Combine(assemblyDirectory, @"..\..\..\..\HttpWebServer.CL\Resources"));
                string filePath = Path.Combine(projectOutputDirectory, "Configuration.json");
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"Configuration file not found at: {filePath}");
                }
                string json = File.ReadAllText(filePath);
                Configuration config = JsonSerializer.Deserialize<Configuration>(json);
                return config;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading the configuration file: {ex.Message}");
                throw;
            }
        }

    }
}
