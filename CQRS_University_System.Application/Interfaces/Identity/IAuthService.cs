using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRS_University_System.Application.DTOs.Auth;

namespace CQRS_University_System.Application.Interfaces.Identity
{
    public interface IAuthService
    {
        Task<RegisterResultDTO> RegiesterAsync(RegisterDTO dto);
        Task<RegisterResultDTO> LoginAsync(LoginDTO dto);

    }
}
