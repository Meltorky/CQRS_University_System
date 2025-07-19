using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRS_University_System.Application.Abstractions.CQRS;
using CQRS_University_System.Application.Commons.Exceptions;
using CQRS_University_System.Application.DTOs.Courses;
using CQRS_University_System.Application.Interfaces;
using CQRS_University_System.Application.Mappers;
using FluentValidation;

namespace CQRS_University_System.Application.Features.Courses.Commands.CreateCourse
{
    public class CreateCourseCommandHandler : ICommandHandler<CreateCourseCommand, CourseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateCourseCommand> _validator;
        public CreateCourseCommandHandler(IUnitOfWork unitOfWork, IValidator<CreateCourseCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<CourseDTO> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var validatorResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                var errors = string.Join(", ", validatorResult.Errors.Select(e => e.ErrorMessage));
                throw new FluentValidation.ValidationException(validatorResult.Errors);
            }

            if (!await _unitOfWork.Departments.IsExist(request.dto.DepartmentId, cancellationToken))
                throw new NotFoundException($"No Department Exist with ID: {request.dto.DepartmentId}");

            var result = await _unitOfWork.Courses.Add(request.dto.ToCourse() ,cancellationToken);
            return result.ToCourseDTO();
        }
    }
}
