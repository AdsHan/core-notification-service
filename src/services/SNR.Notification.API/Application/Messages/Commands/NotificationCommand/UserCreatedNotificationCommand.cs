using FluentValidation;
using SNR.Core.Commands;

namespace SNR.Notification.API.Application.Messages.Commands.NotificationCommand
{

    public class UserCreatedNotificationCommand : Command
    {
        public UserCreatedNotificationCommand(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public string Name { get; set; }
        public string Email { get; set; }

        public override bool Validate()
        {
            BaseResult.ValidationResult = new UserCreatedNotificationValidation().Validate(this);
            return BaseResult.ValidationResult.IsValid;
        }

        public class UserCreatedNotificationValidation : AbstractValidator<UserCreatedNotificationCommand>
        {
            public UserCreatedNotificationValidation()
            {
                RuleFor(c => c.Name)
                    .NotEmpty()
                    .WithMessage("O nome do aluno não foi informado");

                RuleFor(c => c.Email)
                    .Must(TerEmailalido)
                    .WithMessage("O Email informado não é válido.");
            }

            protected static bool TerEmailalido(string email)
            {
                return Core.DomainObjects.Email.Validate(email);
            }

        }
    }
}