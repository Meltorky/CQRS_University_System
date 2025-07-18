using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRS_University_System.Application.DTOs.Students;

namespace CQRS_University_System.Application.DTOs.Departments
{
    public class DepartmentStudentsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<StudentDTO> Students { get; set; } = new();
    }
}
