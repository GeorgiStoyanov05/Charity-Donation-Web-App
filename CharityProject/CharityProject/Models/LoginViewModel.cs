using System.ComponentModel.DataAnnotations;

namespace CharityProject.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        public bool RememberMe { get; set; } = false;

        [UIHint("hidden")]
        public string? ReturnUrl { get; set; }
    }
}
