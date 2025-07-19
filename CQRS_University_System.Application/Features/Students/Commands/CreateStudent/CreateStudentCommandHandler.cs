using CQRS_University_System.Application.Abstractions.CQRS;
using CQRS_University_System.Application.Commons.Exceptions;
using CQRS_University_System.Application.DTOs.Students;
using CQRS_University_System.Application.Interfaces;
using CQRS_University_System.Application.Mappers;
using FluentValidation;

namespace CQRS_University_System.Application.Features.Students.Commands.CreateStudent
{
    public class CreateStudentCommandHandler : ICommandHandler<CreateStudentCommand, StudentDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateStudentCommand> _validator;
        public CreateStudentCommandHandler(IUnitOfWork unitOfWork, IValidator<CreateStudentCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<StudentDTO> Handle(CreateStudentCommand request, CancellationToken token)
        {
            // Manually trigger the validation
            var validationResult = await _validator.ValidateAsync(request, token);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new FluentValidation.ValidationException(validationResult.Errors);
            }

            if (!await _unitOfWork.Departments.IsExist(request.dto.DepartmentId, token))
                throw new NotFoundException($"Department with ID {request.dto.DepartmentId} was not found.");

            var newStudent = await _unitOfWork.Students.Add(request.dto.ToStudentEntity(), token);
            return newStudent.ToStudentDTO();
        }
    }
}
