using CQRS_University_System.Application.Abstractions.CQRS;
using CQRS_University_System.Application.Commons.Exceptions;
using CQRS_University_System.Application.DTOs.Students;
using CQRS_University_System.Application.Interfaces;
using CQRS_University_System.Application.Mappers;
using FluentValidation;

namespace CQRS_University_System.Application.Features.Students.Queries.GetStudentById
{
    public class GetStudentByIdQueryHandler : IQueryHandler<GetStudentByIdQuery,StudentDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<GetStudentByIdQuery> _validator;
        public GetStudentByIdQueryHandler(IValidator<GetStudentByIdQuery> validator, IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<StudentDTO> Handle(GetStudentByIdQuery request, CancellationToken token)
        {
            var validatorResult = await _validator.ValidateAsync(request, token);

            if (!validatorResult.IsValid)
            {
                var errors = string.Join(", ", validatorResult.Errors.Select(e => e.ErrorMessage));
                throw new FluentValidation.ValidationException(validatorResult.Errors);
            }

            var student = await _unitOfWork.Students.GetById(request.Id, token);

            if (student == null)
                throw new NotFoundException($"No Student Exist with ID: {request.Id}");

            return student.ToStudentDTO();
        }
    }
}
