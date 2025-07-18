using CQRS_University_System.Application.Abstractions.CQRS;
using CQRS_University_System.Application.DTOs.Courses;
using CQRS_University_System.Application.Interfaces;
using CQRS_University_System.Application.Mappers;
using CQRS_University_System.Domain.Commons;
using CQRS_University_System.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CQRS_University_System.Application.Features.Students.Queries.GetStudentCourses
{
    public class GetStudentCoursesQueryHandler : IQueryHandler<GetStudentCoursesQuery, List<CourseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetStudentCoursesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CourseDTO>> Handle(GetStudentCoursesQuery request, CancellationToken cancellationToken)
        {
            var filter = new QueryFilterModel<Student>();

            filter.AddInclude(s => s
                .Include(st => st.StudentCourses)
                .ThenInclude(c => c.Course));

            var result = await _unitOfWork.Students.GetById(request.Id,cancellationToken,filter);

            return result is null ?
                throw new ArgumentException() :
                result.StudentCourses.Select(c => c.Course.ToCourseDTO()).ToList();
        }
    }
}
