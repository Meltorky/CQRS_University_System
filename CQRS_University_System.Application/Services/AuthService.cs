using CQRS_University_System.Application.DTOs.Auth;
using CQRS_University_System.Application.Interfaces.Identity;
using CQRS_University_System.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace CQRS_University_System.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
        }

        public async Task<RegisterResultDTO> RegiesterAsync(RegisterDTO dto)
        {
            if (await _userManager.FindByEmailAsync(dto.Email) is not null)
            {
                return new RegisterResultDTO()
                {
                    Message = "Email is already Registed !!"
                };
            }

            var newUser = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(newUser, dto.Password);





            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description}, ";
                }
                return new RegisterResultDTO()
                {
                    Message = errors
                };
            }


            await _userManager.AddToRoleAsync(newUser, "User");
            return await _tokenService.CreateTokenAsync(newUser);
        }


        public async Task<RegisterResultDTO> LoginAsync(LoginDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email.ToLower().Trim());

            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                return new RegisterResultDTO() { Message = "InValid Emai or Password !!" };
            }

            return await _tokenService.CreateTokenAsync(user);
        }
    }

}

