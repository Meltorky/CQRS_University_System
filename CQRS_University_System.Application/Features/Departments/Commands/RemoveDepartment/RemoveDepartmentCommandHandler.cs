using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CQRS_University_System.Application.Abstractions.CQRS;
using CQRS_University_System.Application.Commons.Exceptions;
using CQRS_University_System.Application.Interfaces;

namespace CQRS_University_System.Application.Features.Departments.Commands.RemoveDepartment
{
    public class RemoveDepartmentCommandHandler : ICommandHandler<RemoveDepartmentCommand,bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RemoveDepartmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(RemoveDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _unitOfWork.Departments.GetById(request.Id, cancellationToken);

            if (department is null)
                throw new NotFoundException($"Department with Id {request.Id} not Exist !");

            return await _unitOfWork.Departments.Delete(department, cancellationToken);
        }
    }
}
