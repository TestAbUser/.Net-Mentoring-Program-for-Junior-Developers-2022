using System.Configuration;

namespace GlobalizedConsoleApp.Configuration
{
    /// <summary>
    /// Describes custom section in the application configuration file.
    /// </summary>
    public class CustomConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("appName")]
        public string ApplicationName => (string)base["appName"];

        [ConfigurationProperty("culture")]
        public string Culture => (string)base["culture"];

        [ConfigurationProperty("outputDirectory")]
        public OutputDirectoryElement OutputDirectory => (OutputDirectoryElement)this["outputDirectory"];

        [ConfigurationCollection(typeof(DirectoryElement),
            AddItemName = "directory",
            ClearItemsName = "clear",
            RemoveItemName = "del")]
        [ConfigurationProperty("directories")]
        public DirectoryElementCollection Directories => (DirectoryElementCollection)this["directories"];

        [ConfigurationCollection(typeof(RuleElement),
            AddItemName = "rule",
            ClearItemsName = "clear",
            RemoveItemName = "del")]
        [ConfigurationProperty("rules")]
        public RuleElementCollection Rules => (RuleElementCollection)this["rules"];
    }
}
