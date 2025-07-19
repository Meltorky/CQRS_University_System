using CQRS_University_System.Application.Features.Courses.Queries.GetCourseById;
using FluentValidation;

namespace CQRS_University_System.Application.Features.Courses.Queries.GetCourseStudents
{
    public class GetCourseStudentsValidator : AbstractValidator<GetCourseStudentsQuery>
    {
        public GetCourseStudentsValidator()
        {
            RuleFor(e => e.Id).NotNull().GreaterThan(0);
        }
    }
}
