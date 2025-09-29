using System.ComponentModel.DataAnnotations;

namespace ProfileEditor.Models
{
    public class ProfileViewModel
    {
        [Display(Name = "User name")]
        public string? UserName { get; set; }
        [Display(Name = "First name")]
        public string? FirstName { get; set; }
        [Display(Name = "Last name")]
        public string? LastName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Contact number")]
        public string? ContactNumber { get; set; }
        [Display(Name = "Adress")]
        public string? Adress {  get; set; }
        [Display(Name = "City")]
        public string? City {  get; set; }
        [Display(Name = "Country")]
        public string? Country { get; set; }
        [Display(Name = "Age")]
        public int? Age{ get; set; }
        [Display(Name = "About me")]
        public string? Description { get; set; }
        public IFormFile? PhotoImage { get; set; }
        public string? PhotoImageName { get; set; } // pre zobrazenie existujúceho avataru
        public bool UseDefaultPhoto {  get; set; }
    }
}
