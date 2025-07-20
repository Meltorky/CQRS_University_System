using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace CQRS_University_System.Application.DTOs.Courses
{
    public class CreateCourseDTO
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Max. Length is 100")]
        [SwaggerSchema(Description = "Enter the date in format yyyy-MM-dd")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(10, ErrorMessage = "Max. Length is 10")]
        public string CourseCode { get; set; } = string.Empty;

        [Required]
        [Range(1, 3, ErrorMessage = "Credits are from 1 to 3")]
        public int Credits { get; set; }

        [Required]
        [MaxLength(50)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int DepartmentId { get; set; }
    }
}
