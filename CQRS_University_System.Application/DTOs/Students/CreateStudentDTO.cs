using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS_University_System.Application.DTOs.Students
{
    public class CreateStudentDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public double CGPA { get; set; }

        [Required]
        public int FinishedHours { get; set; }

        [Required]
        public DateOnly DateOfBirth { get; set; }

        [Required]
        public string Gender { get; set; } = string.Empty;

        [Required]
        public string City { get; set; } = string.Empty;

        [Required]
        public int DepartmentId { get; set; }
    }
}
