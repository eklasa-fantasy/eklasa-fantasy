using System.ComponentModel.DataAnnotations;

namespace Identity.API.Dtos
{
    public class RegisterDto
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        [EmailAddress]
        public required string EmailAddress {  get; set; }
        
        [Required]
        public required string Password { get; set; }
    }
}
