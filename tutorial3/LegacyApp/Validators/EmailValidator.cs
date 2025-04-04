namespace LegacyApp.Validators
{
    public class EmailValidator : IUserDetailsValidator
    {
        public bool ValidateUserDetails(UserDetails userDetails)
        {
            return userDetails.Email.Contains("@") && userDetails.Email.Contains(".");
        }
    }
}
