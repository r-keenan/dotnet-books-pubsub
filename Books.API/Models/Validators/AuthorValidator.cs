using FluentValidation;

namespace Books.API.Models.Validators
{
    public class AuthorValidator : AbstractValidator<Author>
    {
        public AuthorValidator()
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(0);
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .Length(3, 75)
                .WithMessage("FirstName must be between 3 and 75 characters");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .Length(3, 75)
                .WithMessage("LastName must be between 3 and 75 characters");
        }
    }
}
