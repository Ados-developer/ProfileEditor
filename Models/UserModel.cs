using Microsoft.AspNetCore.Identity;

namespace ProfileEditor.Models
{
    public class UserModel : IdentityUser
    {
        public ProfileModel Profile { get; set; }
    }
}
