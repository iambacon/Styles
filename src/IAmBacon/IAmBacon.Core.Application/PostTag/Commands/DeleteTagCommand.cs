namespace IAmBacon.Core.Application.PostTag.Commands
{
    public class DeleteTagCommand
    {
        public int Id { get; }

        public DeleteTagCommand(int id)
        {
            Id = id;
        }
    }
}
