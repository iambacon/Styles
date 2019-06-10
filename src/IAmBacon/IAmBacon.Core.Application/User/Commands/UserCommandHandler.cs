using System;
using System.Threading.Tasks;
using IAmBacon.Core.Application.Base;
using IAmBacon.Core.Domain.AggregatesModel.UserAggregate;

namespace IAmBacon.Core.Application.User.Commands
{
    public class UserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        private readonly IUserRepository _repository;

        public UserCommandHandler(IUserRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException("Value cannot be null or whitespace.", nameof(repository));
        }

        public async Task HandleAsync(CreateUserCommand command)
        {
            var entity = new Domain.AggregatesModel.UserAggregate.User(command.FirstName, command.LastName, command.Email);

            _repository.Add(entity);
            await _repository.UnitOfWork.CommitAsync();
        }
    }
}
