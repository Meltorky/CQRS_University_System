using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS_University_System.Domain.Entities
{
    public class Student : BaseEntity
    {
        public double CGPA { get; set; } = 0;


        [Range(0, 140)]
        public int FinishedHours { get; set; }


        [Range(0, 140)]
        public int RemainHours => 140 - FinishedHours;

        public DateOnly DateOfBirth { get; set; }


        [StringLength(6)]
        public string Gender { get; set; } = string.Empty;


        [StringLength(50)]
        public string City { get; set; } = string.Empty;

        // Nav properties

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = default!;

        // many-to-many 
        public ICollection<StudentCourses> StudentCourses { get; set; } = new List<StudentCourses>();
    }
}
