using CQRS_University_System.Application.Abstractions.CQRS;
using CQRS_University_System.Application.Commons.Exceptions;
using CQRS_University_System.Application.DTOs.Courses;
using CQRS_University_System.Application.Interfaces;
using CQRS_University_System.Application.Mappers;
using FluentValidation;

namespace CQRS_University_System.Application.Features.Courses.Queries.GetCourseById
{
    public class GetCourseByIdQueryHandler : IQueryHandler<GetCourseByIdQuery, CourseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<GetCourseByIdQuery> _validator;
        public GetCourseByIdQueryHandler(IValidator<GetCourseByIdQuery> validator, IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<CourseDTO> Handle(GetCourseByIdQuery request, CancellationToken token)
        {
            var validatorResult = await _validator.ValidateAsync(request,token);

            if (!validatorResult.IsValid)
            {
                var errors = string.Join(", ", validatorResult.Errors.Select(e => e.ErrorMessage));
                throw new FluentValidation.ValidationException(validatorResult.Errors);
            }

            var course = await _unitOfWork.Courses.GetById(request.Id,token);

            if (course == null)
                throw new NotFoundException($"No Course Exist with ID: {request.Id}");

            return course.ToCourseDTO();
        }
    }
}
