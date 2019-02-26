using System;
using System.Threading.Tasks;
using IAmBacon.Core.Application.Base;
using IAmBacon.Core.Domain.AggregatesModel.PostAggregate;

namespace IAmBacon.Core.Application.PostTag.Commands
{
    public class TagCommandHandler : ICommandHandler<CreateTagCommand>
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
    }
}
