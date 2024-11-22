using System.Configuration;

namespace GlobalizedConsoleApp.Configuration
{
    /// <summary>
    /// For work with collections of custom "directory" elements.
    /// </summary>
    public class DirectoryElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement() => new DirectoryElement();

        protected override object GetElementKey(ConfigurationElement element) => 
            ((DirectoryElement)element).Key;
    }
}
