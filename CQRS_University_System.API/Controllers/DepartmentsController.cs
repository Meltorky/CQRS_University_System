using CQRS_University_System.Application.Features.Departments.Commands.CreateDepartment;
using CQRS_University_System.Application.Features.Departments.Commands.RemoveDepartment;
using CQRS_University_System.Application.Features.Departments.Queries.GetAllDepartments;
using CQRS_University_System.Application.Features.Departments.Queries.GetDepartmentCourses;
using CQRS_University_System.Application.Features.Departments.Queries.GetDepartmentStudents;
using CQRS_University_System.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CQRS_University_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DepartmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }



        /// <summary>
        /// Retrieves a list of all departments.
        /// </summary>
        /// <param name="cancellationToken">Token to cancel the operation.</param>
        /// <returns>List of all departments.</returns>
        /// <response code="200">Successfully retrieved the department list.</response>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllDepartments(CancellationToken cancellationToken)
        {
            var query = new GetAllDepartmentsQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }



        /// <summary>
        /// Retrieves all departments along with their courses.
        /// </summary>
        /// <param name="token">Token to cancel the operation.</param>
        /// <returns>List of departments with related courses.</returns>
        /// <response code="200">Successfully retrieved departments with their courses.</response>
        [HttpGet("courses")]
        public async Task<IActionResult> GetAllDepartmentCourses(CancellationToken token)
        {
            var query = new GetDepartmentCoursesQuery();
            var result = await _mediator.Send(query, token);
            return Ok(result);
        }



        /// <summary>
        /// Retrieves all departments along with their students.
        /// </summary>
        /// <param name="token">Token to cancel the operation.</param>
        /// <returns>List of departments with related students.</returns>
        /// <response code="200">Successfully retrieved departments with their students.</response>
        [HttpGet("students")]
        public async Task<IActionResult> GetAllDepartmentStudents(CancellationToken token)
        {
            var query = new GetDepartmentStudentsQuery();
            var result = await _mediator.Send(query, token);
            return Ok(result);
        }



        /// <summary>
        /// Creates a new department.
        /// </summary>
        /// <param name="name">The name of the new department.</param>
        /// <param name="token">Token to cancel the operation.</param>
        /// <returns>The newly created department.</returns>
        /// <response code="200">Successfully created the department.</response>
        [HttpPost("create")]
        public async Task<IActionResult> CreateDepartment([FromQuery] string name, CancellationToken token)
        {
            var command = new CreateDepartmentCommand { Name = name };
            var result = await _mediator.Send(command, token);
            return Ok(result);
        }



        /// <summary>
        /// Deletes a department by its ID.
        /// </summary>
        /// <param name="Id">The ID of the department to delete.</param>
        /// <param name="token">Token to cancel the operation.</param>
        /// <returns>A success message if the department was deleted.</returns>
        /// <response code="200">Successfully deleted the department.</response>
        /// <response code="400">Department not found or deletion failed.</response>
        [Authorize(Roles = nameof(Roles.Admin))]
        [HttpDelete("delete/{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int Id, CancellationToken token)
        {
            var command = new RemoveDepartmentCommand() { Id = Id };
            var result = await _mediator.Send(command, token);
            return result ?
                Ok($"Successfully delete Department with ID: {Id}") :
                throw new ArgumentException();
        }
    }
}