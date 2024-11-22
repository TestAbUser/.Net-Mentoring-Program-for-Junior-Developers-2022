using System.Configuration;

namespace GlobalizedConsoleApp.Configuration
{
    /// <summary>
    /// Defines custom "rule" element in the config file.
    /// </summary>
    public class RuleElement : ConfigurationElement
    {
        [ConfigurationProperty("name", DefaultValue = "rule", IsKey = true, IsRequired = false)]
        [RegexStringWrapperValidator("name")]
        public string Name => (string)base["name"];

        [ConfigurationProperty("pattern", DefaultValue = "", IsKey = false, IsRequired = false)]
        public string Pattern => (string)base["pattern"];

        [ConfigurationProperty("targetFolder", DefaultValue = "C:\\targetFolder", IsKey = false, IsRequired = false)]
        [RegexStringWrapperValidator("targetFolder", invalidCharacters: "[<>?*\"/|]")]
        public string TargetFolder => (string)base["targetFolder"];

        [ConfigurationProperty("ordinalNumber", DefaultValue = true, IsRequired = false)]
        public bool OrdinalNumber => (bool)base["ordinalNumber"];

        [ConfigurationProperty("relocationDate", IsKey = false, IsRequired = true)]
        public bool RelocationDate => (bool)base["relocationDate"];
    }
}
