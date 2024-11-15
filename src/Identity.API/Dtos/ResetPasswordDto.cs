using System.ComponentModel.DataAnnotations;

namespace Identity.API.Dtos
{
    public class ResetPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email {get; set;}

        [Required]
        public string Password {get; set;}

        [Required]
        public string Token {get; set;}
    }
}