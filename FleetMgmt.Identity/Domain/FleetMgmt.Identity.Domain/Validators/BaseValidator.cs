using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using FluentValidation.Results;

namespace FleetMgmt.Identity.Domain.Validators
{
    public abstract class BaseValidator<T> : AbstractValidator<T>
    {
        public ValidationResult IsValid(T Request)
        {
            this.CascadeMode = CascadeMode.Continue;
            var validationResult = Validate(Request);
            return validationResult;
        }
        public bool IsLengthValidation(string addr, int length)
        {
            return addr != null && addr.Length > length;
        }

    }
}
