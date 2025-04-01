using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp.Validators
{
    public record UserDetails(string FirstName, string LastName, string Email, DateTime DateOfBirth, int ClientId)
    {
    }
}
