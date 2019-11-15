namespace IAmBacon.Admin.ViewModels.User
{
    public class EditUserViewModel
    {
        public int Id { get; set; }
        public string Bio { get; set; }
        public string ProfileImage { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
    }
}
