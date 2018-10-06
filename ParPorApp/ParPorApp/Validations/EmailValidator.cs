using ParPorApp.Resx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace ParPorApp.Validations
{
    public class EmailValidator : IValidationRule<string>, INotifyPropertyChanged
    {
        const string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
        public string ValidationDescription { get; set; } = AppResources.Required_Field;
        public string Value { get; set; }
        public bool IsValid => Validate(Value);
        public bool Validate(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                ValidationDescription = AppResources.Required_Field;
                return false;
            }
            if (!string.IsNullOrEmpty(value))
            {
                var regex = new Regex(pattern, RegexOptions.IgnoreCase);
                if (!regex.IsMatch(value))
                {
                    ValidationDescription = AppResources.Invalid_Email;
                    //return false;
                }
            }
            return true;
        }
        void OnValueChanged() => propertyChangedCallback?.Invoke();

        readonly Action propertyChangedCallback;

        public EmailValidator(Action propertyChangedCallback = null)
        {
            this.propertyChangedCallback = propertyChangedCallback;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
