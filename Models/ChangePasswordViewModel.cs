using System.ComponentModel.DataAnnotations;

namespace ProfileEditor.Models
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Invalid current password")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Invalid new password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
