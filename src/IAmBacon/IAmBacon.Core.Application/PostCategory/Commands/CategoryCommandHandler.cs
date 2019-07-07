using System;
using System.Threading.Tasks;
using IAmBacon.Core.Application.Base;
using IAmBacon.Core.Domain.AggregatesModel.PostAggregate;

namespace IAmBacon.Core.Application.PostCategory.Commands
{
    /// <summary>
    /// Because this is a small project I am putting all the handlers for the CRUD into one.
    /// The handler's job is to do repository work.
    /// </summary>
    /// <seealso cref="CreateCategoryCommand" />
    public sealed class CategoryCommandHandler : ICommandHandler<CreateCategoryCommand>, ICommandHandler<DeleteCategoryCommand>,
        ICommandHandler<UpdateCategoryCommand>
    {
        private readonly ICategoryRepository _repository;

        public CategoryCommandHandler(ICategoryRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task HandleAsync(CreateCategoryCommand command)
        {
            var entity = new Category(command.Name);

            _repository.Add(entity);
            await _repository.UnitOfWork.CommitAsync();
        }

        public async Task HandleAsync(DeleteCategoryCommand command)
        {
            var entity = await _repository.GetAsync(command.Id);
            entity.SetDelete(true);

            await _repository.UnitOfWork.CommitAsync();
        }

        public async Task HandleAsync(UpdateCategoryCommand command)
        {
            var entity = await _repository.GetAsync(command.Id);

            if (entity is null)
            {
                throw new NullReferenceException("Category could not be found");
            }

            entity.SetName(command.Name);
            entity.SetActive(command.Active);
            entity.SetDelete(command.Deleted);

            _repository.Update(entity);

            await _repository.UnitOfWork.CommitAsync();
        }
    }
}
