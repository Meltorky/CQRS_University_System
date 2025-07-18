using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRS_University_System.Application.Abstractions.CQRS;
using CQRS_University_System.Application.DTOs.Courses;

namespace CQRS_University_System.Application.Features.Students.Queries.GetStudentCourses
{
    public class GetStudentCoursesQuery : IQuery<List<CourseDTO>>
    {
        public int Id { get; set; }
    }
}
