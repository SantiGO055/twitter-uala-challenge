using FluentValidation;

namespace TwitterUalaChallenge.Application.UseCases.v1.Tweet.Commands.CreateTweet;

public class CreateTweetValidator: AbstractValidator<CreateTweetCommand>
{
    public CreateTweetValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("La propiedad es requerida.");


        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("La propiedad es requerida")
            .MaximumLength(280)
            .WithMessage("El contenido del tweet debe ser como maximo 280 caracteres.");
    }
}