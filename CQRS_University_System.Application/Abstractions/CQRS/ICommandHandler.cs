using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CQRS_University_System.Application.Abstractions.CQRS
{
    public interface ICommandHandler<TCommand,TResponse> : IRequestHandler<TCommand,TResponse> 
        where TCommand : IRequest<TResponse>
    {
    }
}
