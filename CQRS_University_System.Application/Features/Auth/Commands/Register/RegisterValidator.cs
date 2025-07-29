using FluentValidation;

namespace CQRS_University_System.Application.Features.Auth.Commands.Register
{
    public class RegisterValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterValidator() 
        {
            // Rule for Email
            RuleFor(x => x.registerDTO.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .MaximumLength(100)
                .WithMessage("Email cannot exceed 100 characters.");


            // Rule for Password
            RuleFor(x => x.registerDTO.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one non-alphanumeric character.");
        }
    }
}
