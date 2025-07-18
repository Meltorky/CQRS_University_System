using CQRS_University_System.Application.Abstractions.CQRS;
using CQRS_University_System.Application.DTOs.Courses;
using CQRS_University_System.Application.DTOs.Departments;
using CQRS_University_System.Application.Interfaces;
using CQRS_University_System.Application.Mappers;
using CQRS_University_System.Domain.Commons;
using CQRS_University_System.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CQRS_University_System.Application.Features.Departments.Queries.GetDepartmentCourses
{
    public class GetDepartmentCoursesQueryHandler : IQueryHandler<GetDepartmentCoursesQuery, List<DepartmentCoursesDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetDepartmentCoursesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<DepartmentCoursesDTO>> Handle(GetDepartmentCoursesQuery request, CancellationToken cancellationToken)
        {
            var filter = new QueryFilterModel<Department>();
            filter.AddInclude(x => x.Include(z => z.Courses));

            var result = await _unitOfWork.Departments.Filter(cancellationToken, filter);

            return result.Select(x => new DepartmentCoursesDTO
            {
                Id = x.Id,
                DepartmentName = x.Name,
                Courses = x.Courses.ToList().ToCourseDTOs()
            }).ToList();    
        }
    }
}
