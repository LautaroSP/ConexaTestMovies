using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conexa.TestMovies.Application.Features.Commands.UpdateMovie
{
    public class UpdateMovieCommandValidator : AbstractValidator<UpdateMovieCommand>
    {
        public UpdateMovieCommandValidator() 
        {
            RuleFor(x => x.id)
                .NotEmpty().WithMessage("Id is required.");
            RuleFor(x => x.movie.Title)
                .NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.movie.Director)
                .NotEmpty().WithMessage("Director is required.");
            RuleFor(x => x.movie.ReleaseDate)
                .NotEmpty().WithMessage("ReleaseDate is required.");
        }
    }
}
