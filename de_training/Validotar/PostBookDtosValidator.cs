using de_training.DTOs;
using FluentValidation;

namespace de_training.Validotar
{
    public class PostBookDtosValidator : AbstractValidator<PostBookDto>
    {
        public PostBookDtosValidator()
        {
            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
