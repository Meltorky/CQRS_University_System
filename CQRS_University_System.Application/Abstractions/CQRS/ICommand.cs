using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CQRS_University_System.Application.Abstractions.CQRS
{
    public interface ICommand<TResponse> : IRequest<TResponse>
    {
    }
}
