using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace CQRS_University_System.Application.Features.Students.Queries.GetStudentCourses
{
    public class GetStudentCoursesValidator : AbstractValidator<GetStudentCoursesQuery>
    {
        public GetStudentCoursesValidator()
        {
            Console.WriteLine("################### Validator ###########################################");
            RuleFor(e => e.Id).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
