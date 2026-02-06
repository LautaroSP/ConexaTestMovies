using FluentValidation;

namespace Conexa.TestMovies.Application.Features.Commands.AddUser
{
    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator() 
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName is required.");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email is required.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
            RuleFor(x => x.IdRole)
                .NotEmpty().WithMessage("IdRole is required.")
                .Must(id => id == 1 || id == 2).WithMessage("IdRole must be 1 or 2");
        }
    }
}
