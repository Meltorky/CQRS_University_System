using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRS_University_System.Application.DTOs.Auth;
using CQRS_University_System.Domain.Identity;

namespace CQRS_University_System.Application.Interfaces.Identity
{
    public interface ITokenService
    {
        Task<RegisterResultDTO> CreateTokenAsync(ApplicationUser user);
    }
}
