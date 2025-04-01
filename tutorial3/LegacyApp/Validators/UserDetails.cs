using System;

namespace LegacyApp.Validators
{
    public record UserDetails(string FirstName, string LastName, string Email, DateTime DateOfBirth, int ClientId)
    {
    }
}
