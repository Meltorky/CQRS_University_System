using CQRS_University_System.Application.Abstractions.CQRS;
using CQRS_University_System.Application.Commons.Exceptions;
using CQRS_University_System.Application.DTOs.Auth;
using CQRS_University_System.Application.Interfaces.Identity;
using CQRS_University_System.Domain.Identity;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace CQRS_University_System.Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler : ICommandHandler<LoginCommand, RegisterResultDTO>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IValidator<LoginCommand> _validator;

        public LoginCommandHandler(UserManager<ApplicationUser> userManager,
                                   ITokenService tokenService,
                                   IValidator<LoginCommand> validator)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _validator = validator;
        }

        public async Task<RegisterResultDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // Validate
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            // Find user
            var user = await _userManager.FindByEmailAsync(request.loginDTO.Email.ToLower().Trim());
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.loginDTO.Password))
            {
                throw new NotFoundException("Invalid Email or Password !!");
            }

            return await _tokenService.CreateTokenAsync(user);
        }
    }

}
