using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp.Validators
{
    internal class NameValidator : IUserDetailsValidator
    {
        public bool ValidateUserDetails(UserDetails userDetails)
        {
            return !(string.IsNullOrEmpty(userDetails.FirstName) || string.IsNullOrEmpty(userDetails.LastName));
        }
    }
}
