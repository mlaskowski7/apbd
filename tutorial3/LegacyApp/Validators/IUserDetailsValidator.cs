using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp.Validators
{
    internal interface IUserDetailsValidator
    {
        bool ValidateUserDetails(UserDetails userDetails);
    }
}
