namespace LegacyApp.Validators
{
    public class NameValidator : IUserDetailsValidator
    {
        public bool ValidateUserDetails(UserDetails userDetails)
        {
            return !(string.IsNullOrEmpty(userDetails.FirstName) || string.IsNullOrEmpty(userDetails.LastName));
        }
    }
}
