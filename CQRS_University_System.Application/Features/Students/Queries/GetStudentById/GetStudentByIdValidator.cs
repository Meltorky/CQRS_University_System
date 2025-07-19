using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace CQRS_University_System.Application.Features.Students.Queries.GetStudentById
{
    public class GetStudentByIdValidator : AbstractValidator<GetStudentByIdQuery>
    {
        public GetStudentByIdValidator()
        {
            RuleFor(x => x.Id).NotNull().GreaterThan(0);
        }
    }
}
