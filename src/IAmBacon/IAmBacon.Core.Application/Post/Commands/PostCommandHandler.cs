using System;
using System.Threading.Tasks;
using IAmBacon.Core.Application.Base;
using IAmBacon.Core.Domain.AggregatesModel.PostAggregate;

namespace IAmBacon.Core.Application.Post.Commands
{
    public class PostCommandHandler: ICommandHandler<CreatePostCommand>
    {
        private readonly IPostRepository _repository;

        public PostCommandHandler(IPostRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task HandleAsync(CreatePostCommand command)
        {
            var entity = new Domain.AggregatesModel.PostAggregate.Post(command.AuthorId, command.CategoryId, command.Title,
                command.Content);

            entity.SetActive(command.IsActive);
            entity.SetImage(command.Image);
            entity.SetNoCss(command.NoCss);

            _repository.Add(entity);
            await _repository.UnitOfWork.CommitAsync();
        }
    }
}
