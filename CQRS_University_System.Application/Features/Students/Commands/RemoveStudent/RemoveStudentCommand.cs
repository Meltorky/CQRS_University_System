using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRS_University_System.Application.Abstractions.CQRS;

namespace CQRS_University_System.Application.Features.Students.Commands.RemoveStudent
{
    public class RemoveStudentCommand : ICommand<bool>
    {
        public int Id { get; set; }
    }
}
