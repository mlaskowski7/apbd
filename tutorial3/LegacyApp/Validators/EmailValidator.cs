using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp.Validators
{
    internal class EmailValidator : IUserDetailsValidator
    {
        public bool ValidateUserDetails(UserDetails userDetails)
        {
            return userDetails.Email.Contains("@") && userDetails.Email.Contains(".");
        }
    }
}
