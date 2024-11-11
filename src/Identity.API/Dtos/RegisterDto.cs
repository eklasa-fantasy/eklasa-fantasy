using System.ComponentModel.DataAnnotations;

namespace Identity.API.Dtos
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string? EmailAddress {  get; set; }
        
        [Required]
        public string? Password { get; set; }
    }
}
