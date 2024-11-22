using System.Configuration;

namespace GlobalizedConsoleApp.Configuration
{
    /// <summary>
    /// For work with the collections of custom "rule" elements.
    /// </summary>
    public class RuleElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement() => new RuleElement();

        protected override object GetElementKey(ConfigurationElement element) => 
            ((RuleElement)element).Name;
    }
}
