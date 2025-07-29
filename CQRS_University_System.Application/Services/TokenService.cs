using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CQRS_University_System.Application.DTOs.Auth;
using CQRS_University_System.Application.Interfaces.Identity;
using CQRS_University_System.Application.Options;
using CQRS_University_System.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


namespace CQRS_University_System.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtOptions jwtOptions;

        public TokenService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JwtOptions> jwt)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            jwtOptions = jwt.Value; // Extract the JwtOptions value from IOptions
        }

        public async Task<RegisterResultDTO> CreateTokenAsync(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Create a list of standard claims for the token
            var claims = new List<Claim>
            {
             new Claim(JwtRegisteredClaimNames.Email, user.Email!), // User's email
             new Claim(JwtRegisteredClaimNames.GivenName, user.UserName!), // User's username
             new Claim(JwtRegisteredClaimNames.Sub, user.UserName!), // Subject (usually the user's unique identifier)
             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Unique token ID
             new Claim("uid", user.Id), // Custom claim for user ID
             new Claim(ClaimTypes.Email, user.Email!), // Email claim (alternative format)
            }
            .Union(userClaims) // Combine with user-specific claims
            .Union(roleClaims); // Combine with role claims

            var tokenHandler = new JwtSecurityTokenHandler();



            // Define the token descriptor (configuration for the token)
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims), // Add all claims to the token
                Issuer = jwtOptions.Issuer,
                Audience = jwtOptions.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)), SecurityAlgorithms.HmacSha256),
                Expires = DateTime.Now.AddDays(double.Parse(jwtOptions.LifeTimeInDays))
            };

            var createdToken = tokenHandler.CreateToken(descriptor);
            var token = tokenHandler.WriteToken(createdToken);

            // Map to RegisterResultDTO
            return new RegisterResultDTO
            {
                Message = "Token created successfully", // Or a more specific message
                IsAuthenticated = true,
                UserName = user.UserName!,
                Email = user.Email!,
                Roles = claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList(),
                Token = token,
                ExpiresOn = createdToken.ValidTo // Or descriptor.Expires if you prefer
            };
        }
    }
}

