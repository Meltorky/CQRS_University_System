using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CQRS_University_System.Application.Abstractions.CQRS;
using CQRS_University_System.Application.DTOs.Courses;

namespace CQRS_University_System.Application.Features.Courses.Commands.CreateCourse
{
    public class CreateCourseCommand : ICommand<CourseDTO>
    {
        public CreateCourseDTO dto { get; set; } = new();
    }
}
