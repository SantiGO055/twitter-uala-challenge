using FluentValidation;

namespace TwitterUalaChallenge.Application.UseCases.v1.Users.Commands.CreateUser;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.UserName)
            .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("El nombre de usuario es requerido.")
            .MaximumLength(25).WithMessage("El nombre de usuario debe contener como máximo 25 caracteres");;

    }
}