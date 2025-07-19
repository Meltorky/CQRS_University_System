using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace CQRS_University_System.Application.Features.Students.Commands.CreateStudent
{
    public class CreateStudentValidator : AbstractValidator<CreateStudentCommand>
    {
        public CreateStudentValidator()
        {
            // Rule for Name
            RuleFor(x => x.dto.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(100)
                .WithMessage("Name cannot exceed 100 characters.");

            // Rule for CGPA
            RuleFor(x => x.dto.CGPA)
                .InclusiveBetween(0, 4.0) // Assuming CGPA is typically out of 4.0, adjust if different (e.g., 5.0)
                .WithMessage("CGPA must be between 0 and 4.0.");

            // Rule for FinishedHours
            RuleFor(x => x.dto.FinishedHours)
                .InclusiveBetween(0, 140)
                .WithMessage("Finished Hours must be between 0 and 140.");

            // Rule for DateOfBirth
            RuleFor(x => x.dto.DateOfBirth)
                .NotEmpty()
                .WithMessage("Date of Birth is required.")
                .LessThan(DateOnly.FromDateTime(DateTime.Today.AddYears(-5))) // Example: Student must be at least 5 years old. Adjust as needed.
                .WithMessage("Student must be at least 5 years old.")
                .LessThan(DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("Date of Birth cannot be in the future.");

            // Rule for Gender
            RuleFor(x => x.dto.Gender)
                .NotEmpty()
                .WithMessage("Gender is required.")
                .Length(1, 6) // Allowing for 'M', 'F', 'Male', 'Female', or other specific short codes
                .WithMessage("Gender must be between 1 and 6 characters long.")
                .Must(BeAValidGender) // Custom validation for specific gender values
                .WithMessage("Gender must be 'Male' or 'Female'.");

            // Rule for City
            RuleFor(x => x.dto.City)
                .NotEmpty()
                .WithMessage("City is required.")
                .MaximumLength(50)
                .WithMessage("City cannot exceed 50 characters.");

            // Rule for DepartmentId
            RuleFor(x => x.dto.DepartmentId)
                .GreaterThan(0) // Assuming DepartmentId should be a positive integer
                .WithMessage("Department ID is required and must be a positive number.");
        }

        // Custom validation method for Gender
        private bool BeAValidGender(string gender)
        {
            // Define your valid gender options here
            var validGenders = new[] { "Male", "Female" }; // Example valid values
            return validGenders.Contains(gender, StringComparer.OrdinalIgnoreCase);
        }
    }
    
}
