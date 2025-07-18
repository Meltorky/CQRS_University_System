using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRS_University_System.Application.DTOs.Courses;

namespace CQRS_University_System.Application.DTOs.Departments
{
    public class DepartmentCoursesDTO
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public List<CourseDTO> Courses { get; set; } = new List<CourseDTO>();
    }
}
