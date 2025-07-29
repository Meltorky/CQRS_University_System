using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRS_University_System.Application.Abstractions.CQRS;
using CQRS_University_System.Application.DTOs.Auth;

namespace CQRS_University_System.Application.Features.Auth.Commands.Login
{
    public class LoginCommand : ICommand<RegisterResultDTO>
    {
        public LoginDTO loginDTO { get; set; } = default!;
    }

}
