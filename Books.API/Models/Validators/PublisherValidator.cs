using FluentValidation;

namespace Books.API.Models.Validators
{
    public class PublisherValidator : AbstractValidator<Publisher>
    {
        public PublisherValidator()
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(3, 75)
                .WithMessage("Name must be between 3 and 100 characters");
            RuleFor(x => x.Address1)
                .NotEmpty()
                .Length(10, 75)
                .WithMessage("Address1 must be between 10 and 75 characters");
            RuleFor(x => x.City)
                .NotEmpty()
                .Length(3, 75)
                .WithMessage("City must be between 3 and 75 characters");
            RuleFor(x => x.ZipCode)
                .NotEmpty()
                .Length(5, 10)
                .WithMessage("ZipCode must be between 5 and 10 characters");
        }
    }
}
