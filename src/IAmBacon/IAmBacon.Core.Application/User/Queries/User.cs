namespace IAmBacon.Core.Application.User.Queries
{
    public class User
    {
        public string Bio { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public int Id { get; set; }

        public string LastName { get; set; }

        public string ProfileImage { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
