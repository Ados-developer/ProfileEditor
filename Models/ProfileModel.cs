namespace ProfileEditor.Models
{
    public class ProfileModel
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string? Description { get; set; }
        public string? ContactNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Adress { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? PhotoImage { get; set; }
        public string UserId { get; set; }
        public UserModel User { get; set; }
    }
}
