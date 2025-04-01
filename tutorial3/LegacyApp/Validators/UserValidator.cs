using LegacyApp.Models;
using System;
using System.Collections.Generic;

namespace LegacyApp.Validators
{
    public class UserValidator : IUserValidator
    {
        private readonly IEnumerable<IUserDetailsValidator> _validators;
        
        public UserValidator(IEnumerable<IUserDetailsValidator> validators)
        {
            _validators = validators
                ?? throw new ArgumentException(nameof(validators));
        }
        public bool ValidateUserCreditLimit(User user)
        {
            return !user.HasCreditLimit || user.CreditLimit >= 500;
        }

        public bool ValidateUserDetails(UserDetails userDetails)
        {
            foreach (var validator in _validators)
            {
                if (!validator.ValidateUserDetails(userDetails)) return false;
            }

            return true;
        }
    }
}
