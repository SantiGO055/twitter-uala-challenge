using FluentValidation;
using TwitterUalaChallenge.Common.Constants;

namespace TwitterUalaChallenge.Application.UseCases.v1.Tests.Commands.CreateTest
{
    public class CreateTestValidator : AbstractValidator<CreateTestCommand>
    {
        public CreateTestValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("La descripcion es obligatoria")
                .MaximumLength(CommonConstants.TEST_DESCRIPTION_MAXIMUM_LENGTH)
                .WithMessage(
                    $"La descripción no debe exceder los {CommonConstants.TEST_DESCRIPTION_MAXIMUM_LENGTH} caracteres.");
        }
    }
}