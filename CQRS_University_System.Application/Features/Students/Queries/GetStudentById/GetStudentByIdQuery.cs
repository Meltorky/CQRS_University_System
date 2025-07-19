using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRS_University_System.Application.Abstractions.CQRS;
using CQRS_University_System.Application.DTOs.Students;

namespace CQRS_University_System.Application.Features.Students.Queries.GetStudentById
{
    public class GetStudentByIdQuery : IQuery<StudentDTO>
    {
        [Required]
        public int Id { get; set; }
    }
}
