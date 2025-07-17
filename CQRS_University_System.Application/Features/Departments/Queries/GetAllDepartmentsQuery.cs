using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CQRS_University_System.Application.Abstractions.CQRS;
using CQRS_University_System.Application.DTOs.Departments;
using CQRS_University_System.Domain.Entities;

namespace CQRS_University_System.Application.Features.Departments.Queries
{
    public class GetAllDepartmentsQuery : IQuery<List<DepartmentDTO>>
    {
    }
}
