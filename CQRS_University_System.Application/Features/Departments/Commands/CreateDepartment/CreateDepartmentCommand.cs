using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CQRS_University_System.Application.Abstractions.CQRS;
using CQRS_University_System.Application.DTOs.Departments;

namespace CQRS_University_System.Application.Features.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentCommand : ICommand<DepartmentDTO>
    {
        public string Name { get; set; } = string.Empty;
    }
}
