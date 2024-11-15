using FluentValidation;

namespace Books.API.Models.Validators
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Title)
                .NotEmpty()
                .Length(3, 100)
                .WithMessage("Title must be between 3 and 100 characters.");
            RuleFor(x => x.PageLength)
                .InclusiveBetween(1, 10_000)
                .WithMessage("PageLength must be between 1 and 10,000.");
            RuleFor(x => x.AuthorId).NotEmpty().WithMessage("AuthorId is required.");
            ;
            RuleFor(x => x.PublisherId).NotEmpty().WithMessage("PublisherId is required.");
        }
    }
}
