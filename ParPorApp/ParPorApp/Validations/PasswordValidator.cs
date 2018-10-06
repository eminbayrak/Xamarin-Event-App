using ParPorApp.Resx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ParPorApp.Validations
{
    public class PasswordValidator : IValidationRule<string>, INotifyPropertyChanged
    {
        const int minLength = 8;
        public string ValidationDescription { get; set; } = AppResources.Required_Field;
        public string Value { get; set; }
        public bool IsValid => Validate(Value);

        public bool Validate(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                ValidationDescription = AppResources.Required_Field;
                return false;
            }
            if (value.Length < minLength)
            {
                ValidationDescription = string.Format(AppResources.Password_Length, minLength);
                return false;
            }
            return true;
        }

        void OnValueChanged() => propertyChangedCallback?.Invoke();

        readonly Action propertyChangedCallback;

        public PasswordValidator(Action propertyChangedCallback = null)
        {
            this.propertyChangedCallback = propertyChangedCallback;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
