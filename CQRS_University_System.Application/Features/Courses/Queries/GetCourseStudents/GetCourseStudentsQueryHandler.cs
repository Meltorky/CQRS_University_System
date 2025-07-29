using CQRS_University_System.Application.Abstractions.CQRS;
using CQRS_University_System.Application.Commons.Exceptions;
using CQRS_University_System.Application.DTOs.Students;
using CQRS_University_System.Application.Interfaces;
using CQRS_University_System.Application.Mappers;
using CQRS_University_System.Domain.Commons;
using CQRS_University_System.Domain.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CQRS_University_System.Application.Features.Courses.Queries.GetCourseStudents
{
    public class GetCourseStudentsQueryHandler : IQueryHandler<GetCourseStudentsQuery, List<StudentDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<GetCourseStudentsQuery> _validator;
        public GetCourseStudentsQueryHandler(IValidator<GetCourseStudentsQuery> validator, IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<StudentDTO>> Handle(GetCourseStudentsQuery request, CancellationToken token)
        {
            var validatorResult = await _validator.ValidateAsync(request, token);

            if (!validatorResult.IsValid)
            {
                var errors = string.Join(", ", validatorResult.Errors.Select(e => e.ErrorMessage));
                throw new FluentValidation.ValidationException(validatorResult.Errors);
            }

            var query = new QueryFilterModel<Course>();
            query.AddInclude(q => q.Include(c => c.StudentCourses).ThenInclude(x => x.Student));

            var course = await _unitOfWork.Courses.GetById(request.Id,token,query);

            if (course == null)
                throw new NotFoundException($"No Course Exist with ID: {request.Id}");

            return course.StudentCourses.Select(sc => sc.Student.ToStudentDTO()).ToList();
        }
    }
}
