using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRS_University_System.Application.Abstractions.CQRS;
using CQRS_University_System.Application.DTOs.Departments;
using CQRS_University_System.Application.Interfaces;
using CQRS_University_System.Domain.Entities;
using MediatR;

namespace CQRS_University_System.Application.Features.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentCommandHandler : ICommandHandler<CreateDepartmentCommand, DepartmentDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateDepartmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DepartmentDTO> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Departments.Add(new Department { Name = request.Name},cancellationToken);
            return new DepartmentDTO 
            {
                Id = result.Id,
                Name = result.Name
            };
        }
    }
}
