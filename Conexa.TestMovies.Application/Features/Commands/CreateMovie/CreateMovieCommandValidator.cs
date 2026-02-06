using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conexa.TestMovies.Application.Features.Commands.CreateMovie
{
    public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
    {
        public CreateMovieCommandValidator() 
        {
            RuleFor(x => x.movie.Title)
                .NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.movie.Director)
                .NotEmpty().WithMessage("Director is required.");
            RuleFor(x => x.movie.ReleaseDate)
                .NotEmpty().WithMessage("ReleaseDate is required.");

        }
    }
}
