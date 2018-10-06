using System;
using System.Collections.Generic;
using System.Text;

namespace ParPorApp.Validations
{
    public interface IValidationRule<T>
    {
        string Value { get; set; }
        string ValidationDescription { get; set; }
        bool Validate(T value);
    }
}
