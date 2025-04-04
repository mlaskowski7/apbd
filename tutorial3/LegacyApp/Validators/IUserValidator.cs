using LegacyApp.Models;

namespace LegacyApp.Validators
{
    public interface IUserValidator
    {
        bool ValidateUserDetails(UserDetails userDetails);
        
        bool ValidateUserCreditLimit(User user);
    }
}
