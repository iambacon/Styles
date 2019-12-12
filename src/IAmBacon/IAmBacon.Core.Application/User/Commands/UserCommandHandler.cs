using System;
using System.Threading.Tasks;
using IAmBacon.Core.Application.Base;
using IAmBacon.Core.Domain.AggregatesModel.UserAggregate;
using IAmBacon.Core.Domain.Utilities;
using Microsoft.AspNetCore.Identity;

namespace IAmBacon.Core.Application.User.Commands
{
    public class UserCommandHandler : ICommandHandler<CreateUserCommand>, ICommandHandler<DeleteUserCommand>,
        ICommandHandler<UpdateUserCommand>
    {
        private readonly IUserRepository _repository;
        private readonly UserManager<IdentityUser> _userManager;

        public UserCommandHandler(IUserRepository repository, UserManager<IdentityUser> userManager)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task HandleAsync(CreateUserCommand command)
        {
            var user = new IdentityUser(command.Email) { Email = command.Email };
            var tempPassword = Password.Generate(32, 12);

            var result = await _userManager.CreateAsync(user, tempPassword);

            if (result.Succeeded)
            {
                var entity = new Domain.AggregatesModel.UserAggregate.User(command.FirstName, command.LastName, command.Email,
                    command.ProfileImage, command.Bio);

                _repository.Add(entity);
                await _repository.UnitOfWork.CommitAsync();
            }
        }

        public async Task HandleAsync(DeleteUserCommand command)
        {
            var entity = await _repository.GetAsync(command.Id);
            entity.SetDelete(true);

            var user = await _userManager.FindByEmailAsync(command.Email);
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                await _repository.UnitOfWork.CommitAsync();
            }
            else
            {
                throw new Exception(result.Errors.ToString());
            }
        }

        public async Task HandleAsync(UpdateUserCommand command)
        {
            var entity = await _repository.GetAsync(command.Id);

            entity.SetActive(command.Active);
            entity.SetDelete(command.Deleted);
            entity.SetEmail(command.Email);
            entity.SetBio(command.Bio);
            entity.SetFirstName(command.FirstName);
            entity.SetLastName(command.LastName);
            entity.SetProfileImage(command.ProfileImage);

            _repository.Update(entity);

            await _repository.UnitOfWork.CommitAsync();
        }
    }
}
