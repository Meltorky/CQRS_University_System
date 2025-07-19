using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace CQRS_University_System.Application.Features.Courses.Commands.CreateCourse
{
    public class CreateCourseValidator : AbstractValidator<CreateCourseCommand>
    {
        public CreateCourseValidator() 
        {
            // Rule for Name
            RuleFor(x => x.dto.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(100)
                .WithMessage("Name cannot exceed 100 characters.");

            // Rule for Credits
            RuleFor(x => x.dto.Credits)
                .InclusiveBetween(1, 3)
                .WithMessage("Credits Hours must be between 1 and 3.");

            // Rule for CourseCode
            RuleFor(x => x.dto.CourseCode)
                .NotEmpty()
                .WithMessage("CourseCode is required.")
                .Length(1, 10)
                .WithMessage("CourseCode must be between 1 and 6 characters long.");

            // Rule for Description
            RuleFor(x => x.dto.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MaximumLength(50)
                .WithMessage("Description cannot exceed 50 characters.");

            // Rule for DepartmentId
            RuleFor(x => x.dto.DepartmentId)
                .GreaterThan(0)
                .WithMessage("Department ID is required and must be a positive number.");
        }
    }
    
}
