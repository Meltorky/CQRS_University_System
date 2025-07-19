using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace CQRS_University_System.Application.Features.Courses.Queries.GetCourseById
{
    public class GetCourseByIdValidator : AbstractValidator<GetCourseByIdQuery>
    {
        public GetCourseByIdValidator()
        {
            RuleFor(e => e.Id).NotNull().GreaterThan(0);
        }
    }
}
