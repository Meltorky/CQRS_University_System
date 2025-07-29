using CQRS_University_System.Application.Abstractions.CQRS;
using CQRS_University_System.Application.DTOs.Auth;
using CQRS_University_System.Application.Interfaces.Identity;
using CQRS_University_System.Domain.Enums;
using CQRS_University_System.Domain.Identity;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace CQRS_University_System.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler : ICommandHandler<RegisterCommand, RegisterResultDTO>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IValidator<RegisterCommand> _validator;
        public RegisterCommandHandler(UserManager<ApplicationUser> userManager, ITokenService tokenService, IValidator<RegisterCommand> validator)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _validator = validator;
        }

        public async Task<RegisterResultDTO> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // Manually trigger the validation
            var validationResult = await _validator.ValidateAsync(request , cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(validationResult.Errors);
            }

            // register a new user

            if (await _userManager.FindByEmailAsync(request.registerDTO.Email) is not null)
            {
                throw new ArgumentException("Email is already exist !!");
            }

            var newUser = new ApplicationUser
            {
                UserName = request.registerDTO.Email,
                Email = request.registerDTO.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(newUser, request.registerDTO.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new RegisterResultDTO { Message = errors };
            }

            await _userManager.AddToRoleAsync(newUser, Roles.Student.ToString());
            return await _tokenService.CreateTokenAsync(newUser);
        }
    }
}
