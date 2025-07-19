using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CQRS_University_System.Application.Abstractions.CQRS;

namespace CQRS_University_System.Application.Features.Courses.Commands.RemoveCourse
{
    public class RemoveCourseCommand : ICommand<bool>
    {
        public int Id { get; set; }
    }
}
