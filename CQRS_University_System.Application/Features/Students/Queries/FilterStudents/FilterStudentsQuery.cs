using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRS_University_System.Application.Abstractions.CQRS;
using CQRS_University_System.Application.DTOs.Students;
using CQRS_University_System.Domain.Commons;
using CQRS_University_System.Domain.Entities;

namespace CQRS_University_System.Application.Features.Students.Queries.FilterStudents
{
    public class FilterStudentsQuery : IQuery<List<StudentDTO>>
    {
        public QueryFilterModel<Student> filterModel { get; set; } = new();
    }
}
