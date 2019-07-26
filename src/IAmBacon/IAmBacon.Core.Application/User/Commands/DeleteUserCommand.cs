namespace IAmBacon.Core.Application.User.Commands
{
    public class DeleteUserCommand
    {
        public int Id { get; }

        public DeleteUserCommand(int id)
        {
            Id = id;
        }
    }
}
