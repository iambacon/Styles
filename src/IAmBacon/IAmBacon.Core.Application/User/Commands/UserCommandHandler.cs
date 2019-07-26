using System;
using System.Threading.Tasks;
using IAmBacon.Core.Application.Base;
using IAmBacon.Core.Domain.AggregatesModel.UserAggregate;

namespace IAmBacon.Core.Application.User.Commands
{
    public class UserCommandHandler : ICommandHandler<CreateUserCommand>, ICommandHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _repository;

        public UserCommandHandler(IUserRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task HandleAsync(CreateUserCommand command)
        {
            var entity = new Domain.AggregatesModel.UserAggregate.User(command.FirstName, command.LastName, command.Email,
                command.ProfileImage, command.Bio);

            _repository.Add(entity);
            await _repository.UnitOfWork.CommitAsync();
        }

        public async Task HandleAsync(DeleteUserCommand command)
        {
            var entity = await _repository.GetAsync(command.Id);
            entity.SetDelete(true);

            await _repository.UnitOfWork.CommitAsync();
        }
    }
}
