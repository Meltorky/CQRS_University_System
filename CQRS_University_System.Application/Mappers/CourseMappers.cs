using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRS_University_System.Application.DTOs.Courses;
using CQRS_University_System.Domain.Entities;

namespace CQRS_University_System.Application.Mappers
{
    public static class CourseMappers
    {
        public static List<CourseDTO> ToCourseDTOs(this List<Course> courses)
        {
            var dtos = new List<CourseDTO>();
            foreach (var course in courses)
            {
                dtos.Add(new CourseDTO()
                {
                    Id = course.Id,
                    Name = course.Name,
                    Credits = course.Credits,
                    CourseCode = course.CourseCode,
                    Description = course.Description,
                });
            }
            return dtos; 
        }

        public static CourseDTO ToCourseDTO(this Course course)
        {
            return new CourseDTO() 
            {
                Id = course.Id,
                Name = course.Name,
                Credits = course.Credits,
                CourseCode = course.CourseCode,
                Description = course.Description,              
            };
        }
    }
}
