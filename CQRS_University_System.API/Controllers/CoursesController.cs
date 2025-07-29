using CQRS_University_System.Application.DTOs.Courses;
using CQRS_University_System.Application.Features.Courses.Commands.CreateCourse;
using CQRS_University_System.Application.Features.Courses.Commands.RemoveCourse;
using CQRS_University_System.Application.Features.Courses.Queries.GetCourseById;
using CQRS_University_System.Application.Features.Courses.Queries.GetCourseStudents;
using CQRS_University_System.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CQRS_University_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CoursesController(IMediator mediator)
        {
            _mediator = mediator;
        }



        /// <summary>
        /// Retrieves the details of a course by its unique ID.
        /// </summary>
        /// <param name="Id">The unique identifier of the course.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>The course details.</returns>
        /// <response code="200">Returns the course details.</response>
        /// <response code="404">If the course is not found.</response>
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCourseById([FromRoute] int Id, CancellationToken token)
        {
            var query = new GetCourseByIdQuery() { Id = Id };
            var result = await _mediator.Send(query, token);
            return Ok(result);
        }



        /// <summary>
        /// Retrieves a list of students enrolled in the specified course.
        /// </summary>
        /// <param name="Id">The unique identifier of the course.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>A list of enrolled students.</returns>
        /// <response code="200">Returns the list of students.</response>
        /// <response code="404">If the course or students are not found.</response>
        [HttpGet("{Id}/students")]
        public async Task<IActionResult> GetCourseStudents([FromRoute] int Id, CancellationToken token)
        {
            var query = new GetCourseStudentsQuery() { Id = Id };
            var result = await _mediator.Send(query, token);
            return Ok(result);
        }



        /// <summary>
        /// Creates a new course with the provided data.
        /// </summary>
        /// <param name="dto">The data for the course to create.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>The newly created course.</returns>
        /// <response code="201">Returns the newly created course.</response>
        /// <response code="400">If the request data is invalid.</response>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] CreateCourseDTO dto, CancellationToken token)
        {
            var command = new CreateCourseCommand() { dto = dto };
            var result = await _mediator.Send(command, token);
            return CreatedAtAction(
                nameof(GetCourseById),
                new { Id = result.Id },
                result);
        }



        /// <summary>
        /// Deletes the specified course by its ID.
        /// </summary>
        /// <param name="Id">The unique identifier of the course to delete.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>A success message if deletion is successful.</returns>
        /// <response code="200">If the course was successfully deleted.</response>
        /// <response code="404">If the course was not found.</response>
        [Authorize(Roles = nameof(Roles.Admin))]
        [HttpDelete("delete/{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int Id, CancellationToken token)
        {
            var command = new RemoveCourseCommand() { Id = Id };
            var result = await _mediator.Send(command, token);
            return result
                ? Ok($"Successfully deleted course with ID: {Id}")
                : throw new ArgumentException(); // Ideally handled by your global exception handler
        }
    }
}
