using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRS_University_System.Application.Abstractions.CQRS;
using CQRS_University_System.Application.DTOs.Students;

namespace CQRS_University_System.Application.Features.Students.Queries.GetAllStudents
{
    public class GetAllStudentsQuery : IQuery<List<StudentDTO>>
    {
    }
}
