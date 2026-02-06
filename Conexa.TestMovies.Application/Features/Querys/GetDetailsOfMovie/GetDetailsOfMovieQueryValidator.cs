using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conexa.TestMovies.Application.Features.Querys.GetDetailsOfMovie
{
    public class GetDetailsOfMovieQueryValidator : AbstractValidator<GetDetailsOfMovieQuery>
    {
        public GetDetailsOfMovieQueryValidator()
        {
            RuleFor(x => x.id)
                .NotEmpty().WithMessage("Id is required.");
        }
    }
}
