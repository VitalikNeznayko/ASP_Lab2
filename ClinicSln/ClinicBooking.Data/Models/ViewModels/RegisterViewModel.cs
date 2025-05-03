using System.ComponentModel.DataAnnotations;

namespace ClinicBooking.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Ім'я")]
        public string FirstName { get; set; } = "";

        [Display(Name = "Прізвище")]
        public string LastName { get; set; } = "";
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; } = "";
    }
}
