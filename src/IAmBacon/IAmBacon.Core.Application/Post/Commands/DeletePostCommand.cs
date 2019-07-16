namespace IAmBacon.Core.Application.Post.Commands
{
    public class DeletePostCommand
    {
        public int Id { get; }

        public DeletePostCommand(int id)
        {
            Id = id;
        }
    }
}
