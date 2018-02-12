namespace IAmBacon.Core.Application.PostCategory.Commands
{
    public class DeleteCategoryCommand
    {
        public int Id { get; }

        public DeleteCategoryCommand(int id)
        {
            Id = id;
        }
    }
}
