using System.Configuration;

namespace GlobalizedConsoleApp.Configuration
{
    /// <summary>
    /// Defines custom "outputDirectory" element in the config file.
    /// </summary>
    public class OutputDirectoryElement : ConfigurationElement
    {
        [ConfigurationProperty("path", DefaultValue = "C:\\Default", IsRequired = false)]
        [RegexStringWrapperValidator("path", invalidCharacters: "[<>? *\"/|]")]
        public string Path => (string)this["path"];
    }
}
