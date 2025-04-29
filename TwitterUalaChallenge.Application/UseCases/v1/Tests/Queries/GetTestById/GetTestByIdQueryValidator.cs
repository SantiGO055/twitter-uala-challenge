using FluentValidation;

namespace TwitterUalaChallenge.Application.UseCases.v1.Tests.Queries.GetTestById
{
    public class GetTestByIdQueryValidator : AbstractValidator<GetTestByIdQuery>
    {
        public GetTestByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("El valor debe ser mayor que 0.");
        }
    }
}