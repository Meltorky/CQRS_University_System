using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRS_University_System.Application.Abstractions.CQRS;
using CQRS_University_System.Application.DTOs.Departments;
using CQRS_University_System.Application.Interfaces;
using CQRS_University_System.Domain.Entities;
using FluentValidation;
using MediatR;

namespace CQRS_University_System.Application.Features.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentCommandHandler : ICommandHandler<CreateDepartmentCommand, DepartmentDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateDepartmentCommand> _validator;
        public CreateDepartmentCommandHandler(IUnitOfWork unitOfWork, IValidator<CreateDepartmentCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<DepartmentDTO> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            // Manually trigger the validation
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new FluentValidation.ValidationException(validationResult.Errors);
            }

            var result = await _unitOfWork.Departments.Add(new Department { Name = request.Name},cancellationToken);
            return new DepartmentDTO 
            {
                Id = result.Id,
                Name = result.Name
            };
        }
    }
}
