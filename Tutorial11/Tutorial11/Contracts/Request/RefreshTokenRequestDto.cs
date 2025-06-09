using System.ComponentModel.DataAnnotations;

namespace Tutorial11.Contracts.Request
{
    public class RefreshTokenRequestDto
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
