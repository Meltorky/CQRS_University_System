using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS_University_System.Domain.Entities
{
    public class Course : BaseEntity
    {
        [MaxLength(10, ErrorMessage = "Max. Length is 10")]
        public string CourseCode { get; set; } = string.Empty; // e.g., "CS0125", "MATH0125"


        [Range(1, 3, ErrorMessage = "Credits are from 1 to 3")]
        public int Credits { get; set; }


        [MaxLength(50)]
        public string Description { get; set; } = string.Empty;


        // Nav properties

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = new();

        // many-to-many 
        public ICollection<StudentCourses> StudentCourses { get; set; } = new List<StudentCourses>();

    }
}
