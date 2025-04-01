using LegacyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp.Validators
{
    public interface IUserValidator
    {
        bool ValidateUserDetails(UserDetails userDetails);
        
        bool ValidateUserCreditLimit(User user);
    }
}
