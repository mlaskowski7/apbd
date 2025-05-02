using Tutorial7.Models;

namespace Tutorial7.Contracts.Response;

public record ClientResponseDto(
    int IdClient, 
    string FirstName, 
    string LastName, 
    string Email, 
    string Telephone, 
    string Pesel)
{
}