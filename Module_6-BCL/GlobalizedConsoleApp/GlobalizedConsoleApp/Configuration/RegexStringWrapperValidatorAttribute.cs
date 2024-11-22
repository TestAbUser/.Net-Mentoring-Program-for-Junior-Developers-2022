using System.Configuration;

namespace GlobalizedConsoleApp.Configuration
{
    /// <summary>
    /// Defines validation attributes that check that elements in the custom section of the config 
    /// have correctly named properties and the values of those properties have no invalid characters.
    /// </summary>
    public sealed class RegexStringWrapperValidatorAttribute : ConfigurationValidatorAttribute
    {
        private readonly string _propertyName;
        private readonly string _invalidCharacters;

        public RegexStringWrapperValidatorAttribute(string propertyName)
        {
            _propertyName = propertyName;
        }

        public RegexStringWrapperValidatorAttribute(string propertyName, string invalidCharacters) : this(propertyName)
        {
            _invalidCharacters = invalidCharacters;
        }

        public string InvalidCharacters => _invalidCharacters;

        public string PropertyName => _propertyName;

        public override ConfigurationValidatorBase ValidatorInstance => new RegexStringWrapperValidator(_propertyName, _invalidCharacters);
    }
}


