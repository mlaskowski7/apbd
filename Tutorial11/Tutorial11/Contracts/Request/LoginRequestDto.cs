using System.ComponentModel.DataAnnotations;

namespace Tutorial11.Contracts.Request
{
    public class LoginRequestDto
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
