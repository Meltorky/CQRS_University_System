using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRS_University_System.Application.Abstractions.CQRS;
using CQRS_University_System.Application.DTOs.Courses;
using CQRS_University_System.Application.DTOs.Departments;

namespace CQRS_University_System.Application.Features.Departments.Queries.GetDepartmentCourses
{
    public class GetDepartmentCoursesQuery : IQuery<List<DepartmentCoursesDTO>>
    {
    }
}
