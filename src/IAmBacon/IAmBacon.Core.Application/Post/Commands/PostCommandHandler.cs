using System;
using System.Threading.Tasks;
using IAmBacon.Core.Application.Base;
using IAmBacon.Core.Domain.AggregatesModel.PostAggregate;

namespace IAmBacon.Core.Application.Post.Commands
{
    public class PostCommandHandler : ICommandHandler<CreatePostCommand>, ICommandHandler<UpdatePostCommand>
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
            entity.SetTags(command.TagIds);

            _repository.Add(entity);

            await _repository.UnitOfWork.CommitAsync();

            foreach (var tagId in command.TagIds)
            {
                var postTagEntity = new Domain.AggregatesModel.PostAggregate.PostTag(entity.Id, tagId);
                _repository.Add(postTagEntity);
            }

            await _repository.UnitOfWork.CommitAsync();
        }

        public async Task HandleAsync(UpdatePostCommand command)
        {
            var entity = await _repository.GetAsync(command.Id);

            if (entity is null)
            {
                throw new NullReferenceException("Post could not be found");
            }

            entity.SetDelete(command.IsDeleted);
            entity.SetActive(command.IsActive);
            entity.SetImage(command.Image);
            entity.SetNoCss(command.NoCss);
            entity.SetTags(command.TagIds);
            entity.SetAuthor(command.AuthorId);
            entity.SetCategory(command.CategoryId);
            entity.SetTitle(command.Title);
            entity.SetContent(command.Content);

            _repository.Update(entity);

            await _repository.UnitOfWork.CommitAsync();
        }
    }
}
