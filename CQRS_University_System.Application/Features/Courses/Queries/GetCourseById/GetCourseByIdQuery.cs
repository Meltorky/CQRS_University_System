using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRS_University_System.Application.Abstractions.CQRS;
using CQRS_University_System.Application.DTOs.Courses;

namespace CQRS_University_System.Application.Features.Courses.Queries.GetCourseById
{
    public class GetCourseByIdQuery : IQuery<CourseDTO>
    {
        [Required]
        public int Id { get; set; }
    }
}
