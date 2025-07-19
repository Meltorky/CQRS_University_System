using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS_University_System.Domain.Entities
{
    public class StudentCourses
    {
        public int StudentId { get; set; }
        public Student Student { get; set; } = default!;

        public int CourseId { get; set; }
        public Course Course { get; set; } = default!;
    }
}
