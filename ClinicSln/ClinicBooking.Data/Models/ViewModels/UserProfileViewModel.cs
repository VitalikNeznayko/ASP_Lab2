using System.ComponentModel.DataAnnotations;

namespace ClinicBooking.Models.ViewModels
{
    public class UserProfileViewModel
    {
        [EmailAddress]
        public string Email { get; set; } = "";

        [Display(Name = "Ім'я")]
        public string FirstName { get; set; } = "";

        [Display(Name = "Прізвище")]
        public string LastName { get; set; } = "";

        [Display(Name = "Телефон")]
        public string? PhoneNumber { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Новий пароль")]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Підтвердіть новий пароль")]
        [Compare("NewPassword", ErrorMessage = "Паролі не співпадають.")]
        public string? ConfirmPassword { get; set; }
    }
}