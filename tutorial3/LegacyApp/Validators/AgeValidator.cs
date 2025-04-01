using LegacyApp.DateTimeProviders;
using System;

namespace LegacyApp.Validators
{
    public class AgeValidator : IUserDetailsValidator
    {
        private const int MinimumAge = 21;

        private readonly IDateTimeProvider _dateTimeProvider;

        public AgeValidator(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider 
                ?? throw new ArgumentException(nameof(dateTimeProvider));
        }

        public bool ValidateUserDetails(UserDetails userDetails)
        {
            var now = _dateTimeProvider.Now;
            var dob = userDetails.DateOfBirth;
            int age = now.Year - dob.Year;
            if (now.Month < dob.Month || (now.Month == dob.Month && now.Day < dob.Day)) age--;

            return age >= MinimumAge;
        }
    }
}
