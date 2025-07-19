using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CQRS_University_System.Application.Abstractions.CQRS;
using CQRS_University_System.Application.DTOs.Courses;
using CQRS_University_System.Application.DTOs.Students;

namespace CQRS_University_System.Application.Features.Courses.Queries.GetCourseStudents
{
    public class GetCourseStudentsQuery : IQuery<List<StudentDTO>>
    {
        [Required]
        public int Id { get; set; }
    }
}
