using System.Configuration;

namespace GlobalizedConsoleApp.Configuration
{
    /// <summary>
    /// Defines custom "directory" element in the config file.
    /// </summary>
    public class DirectoryElement : ConfigurationElement
    {
        [ConfigurationProperty("key", DefaultValue = "key", IsKey = false, IsRequired = false)]
        [RegexStringWrapperValidator("key")]
        public string Key => (string)base["key"];

        [ConfigurationProperty("path", DefaultValue = "C:\\Default", IsKey = false, IsRequired = false)]
        [RegexStringWrapperValidator("path", invalidCharacters: "[<>?*\"/|]")]
        public string Path => (string)base["path"];
    }
}
