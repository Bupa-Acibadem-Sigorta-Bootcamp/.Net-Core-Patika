using System;
using FluentValidation;

namespace WebApi.Applications.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(x => x.Model.Title).NotEmpty().MinimumLength(4);
            RuleFor(x => x.Model.GenreId).GreaterThan(0);
            RuleFor(x => x.Model.PageCount).GreaterThan(0);
            RuleFor(x => x.Model.PublishDate).NotEmpty().LessThan(DateTime.Now.Date);
        }
    }
}