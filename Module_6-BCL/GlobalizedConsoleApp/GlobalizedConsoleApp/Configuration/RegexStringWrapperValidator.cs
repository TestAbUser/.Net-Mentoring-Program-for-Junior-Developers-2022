using System;
using System.Configuration;
using System.Text.RegularExpressions;
using resources = GlobalizedConsoleApp.Resources.Messages;

namespace GlobalizedConsoleApp.Configuration
{
    /// <summary>
    /// Contains validation logic for RegexStringWrapperValidatorAttribute class.
    /// </summary>
    public class RegexStringWrapperValidator : ConfigurationValidatorBase
    {
        public RegexStringWrapperValidator(string propertyName)
        {
            _propertyName = propertyName;
        }

        public RegexStringWrapperValidator(string propertyName, string invalidCharacters)
        {
            pattern = invalidCharacters;
            _propertyName = propertyName;
        }

        private readonly string _propertyName;
        private readonly string pattern;

        public override bool CanValidate(Type type)
        {
            return (type == typeof(string));
        }

        public override void Validate(object value)
        {
            string stringVal = (string)value;
            bool customSectionIsInvalid = false;

            if (string.IsNullOrWhiteSpace(stringVal))
            {
                customSectionIsInvalid = true;
                Console.WriteLine(string.Format(resources.PropertyIsEmpty, _propertyName));
            }
            if (pattern != null)
            {
                if (Regex.IsMatch(stringVal, pattern))
                {
                    customSectionIsInvalid = true;
                    Console.WriteLine(string.Format(resources.HasForbiddenCharacters, _propertyName, stringVal));
                }
            }
            if (customSectionIsInvalid)
            {
                Console.WriteLine(resources.ExitAppMessage);
                Console.ReadKey();
                Environment.Exit(0);
            }
        }
    }
}
