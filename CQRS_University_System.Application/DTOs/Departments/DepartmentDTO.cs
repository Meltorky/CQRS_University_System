using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS_University_System.Application.DTOs.Departments
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
