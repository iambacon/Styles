using System;
using System.Threading.Tasks;
using IAmBacon.Core.Application.Base;
using IAmBacon.Core.Domain.AggregatesModel.PostAggregate;

namespace IAmBacon.Core.Application.PostTag.Commands
{
    public class TagCommandHandler : ICommandHandler<CreateTagCommand>, ICommandHandler<UpdateTagCommand>, ICommandHandler<DeleteTagCommand>
    {
        private readonly ITagRepository _repository;

        public TagCommandHandler(ITagRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task HandleAsync(CreateTagCommand command)
        {
            var entity = new Tag(command.Name);

            _repository.Add(entity);
            await _repository.UnitOfWork.CommitAsync();
        }

        public async Task HandleAsync(UpdateTagCommand command)
        {
            var entity = await _repository.GetAsync(command.Id);

            if (entity is null)
            {
                throw new NullReferenceException("Tag cound not be found");
            }

            entity.SetName(command.Name);
            entity.SetActive(command.Active);
            entity.SetDelete(command.Deleted);

            _repository.Update(entity);

            await _repository.UnitOfWork.CommitAsync();
        }

        public async Task HandleAsync(DeleteTagCommand command)
        {
            var entity = await _repository.GetAsync(command.Id);
            entity.SetDelete(true);

            await _repository.UnitOfWork.CommitAsync();
        }
    }
}
