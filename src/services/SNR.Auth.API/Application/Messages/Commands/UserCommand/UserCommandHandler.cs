using MediatR;
using SNR.Auth.API.Application.DTO;
using SNR.Auth.Domain.Entities;
using SNR.Auth.Domain.Repositories;
using SNR.Auth.Infrastructure.Data;
using SNR.Core.Commands;
using SNR.Core.Communication;
using SNR.Core.Settings;
using SNR.MessageBus;
using SNR.MessageBus.Integration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SNR.Auth.API.Application.Messages.Commands.UserCommand
{
    public class UserCommandHandler : CommandHandler,
        IRequestHandler<AddUserCommand, BaseResult>,
        IRequestHandler<SignInUserCommand, BaseResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenSettings _tokenSettings;
        private readonly IMessageBusService _messageBusService;

        private readonly AuthDbContext _dbContext;

        public UserCommandHandler(IUserRepository userRepository, AuthDbContext dbContext, TokenSettings tokenSettings, IMessageBusService messageBusService)
        {
            _userRepository = userRepository;
            _dbContext = dbContext;
            _tokenSettings = tokenSettings;
            _messageBusService = messageBusService;
        }

        public async Task<BaseResult> Handle(AddUserCommand command, CancellationToken cancellationToken)
        {
            if (!command.Validate()) return command.BaseResult;

            var isIncluded = await _userRepository.GetByUserNameAsync(command.Email);

            if (isIncluded != null)
            {
                AddError("Este nome de usuário já está em uso por outro usuário!");
                return BaseResult;
            }

            var user = new UserModel()
            {
                UserName = command.Email,
                Email = command.Email,
                PhoneNumber = command.Phone,
                EmailConfirmed = true
            };

            var result = await _userRepository.CreateAsync(user, command.Password);

            if (!result.Succeeded)
            {
                AddError("Não foi possível incluir o usuário!");
                return BaseResult;
            }

            var evt = new UserCreatedIntegrationEvent(command.Name, command.Email);

            _messageBusService.Publish(ExchangeType.NOTIFICATION, QueueTypes.NOTIFICATION_USER_CREATED, evt);

            return BaseResult;
        }

        public async Task<BaseResult> Handle(SignInUserCommand command, CancellationToken cancellationToken)
        {
            if (!command.Validate()) return command.BaseResult;

            var isIncluded = await _userRepository.GetByUserNameAsync(command.Email);

            if (isIncluded == null)
            {
                AddError("Este usuário não existe!");
                return BaseResult;
            }

            var result = await _userRepository.SignInAsync(command.Email, command.Password);

            if (result.Succeeded)
            {
                var expiration = _tokenSettings.ExpireHours;
                var token = _userRepository.GenerateToken(command.Email);
                var expirationDate = DateTime.UtcNow.AddHours(expiration);

                // Retorna o token e demais informações
                var response = new LoginTokenDTO
                {
                    Authenticated = true,
                    Token = token,
                    Expiration = expirationDate,
                    Message = "Token JWT OK",
                };

                BaseResult.response = response;
                return BaseResult;
            }

            if (result.IsLockedOut)
            {
                AddError("Usuário temporariamente bloqueado por tentativas inválidas");
                return BaseResult;
            }

            AddError("Usuário ou Senha incorretos");
            return BaseResult;
        }

    }
}