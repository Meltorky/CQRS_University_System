using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS_University_System.Application.DTOs.Students
{
    public class StudentDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public double CGPA { get; set; }

        public int FinishedHours { get; set; }

        public int RemainHours => 140 - FinishedHours;

        public DateOnly DateOfBirth { get; set; }

        public string Gender { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;
    }
}
