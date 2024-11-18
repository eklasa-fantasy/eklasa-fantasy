using System.ComponentModel.DataAnnotations;

namespace Identity.API.Dtos
{
    public class ForgotPasswordDto
    {
        [Required]
        public string Email { get; set; }
    }
}
