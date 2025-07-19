using CQRS_University_System.Application.Abstractions.CQRS;
using CQRS_University_System.Application.Commons.Exceptions;
using CQRS_University_System.Application.DTOs.Courses;
using CQRS_University_System.Application.Interfaces;
using CQRS_University_System.Application.Mappers;
using CQRS_University_System.Domain.Commons;
using CQRS_University_System.Domain.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CQRS_University_System.Application.Features.Students.Queries.GetStudentCourses
{
    public class GetStudentCoursesQueryHandler : IQueryHandler<GetStudentCoursesQuery, List<CourseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<GetStudentCoursesQuery> _validator;

        public GetStudentCoursesQueryHandler(IUnitOfWork unitOfWork, IValidator<GetStudentCoursesQuery> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<List<CourseDTO>> Handle(GetStudentCoursesQuery request, CancellationToken cancellationToken)
        {
            // Manually trigger the validation
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Validation failed: {errors}");
            }

            var filter = new QueryFilterModel<Student>();

            filter.AddInclude(s => s
                .Include(st => st.StudentCourses)
                .ThenInclude(c => c.Course));

            var result = await _unitOfWork.Students.GetById(request.Id,cancellationToken,filter);

            return result is null ?
                throw new NotFoundException($"Student with ID {request.Id} was not found.") :
                result.StudentCourses.Select(c => c.Course.ToCourseDTO()).ToList();
        }
    }
}
