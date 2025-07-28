using CQRS_University_System.Application.DTOs.Students;
using CQRS_University_System.Application.Features.Students.Commands.CreateStudent;
using CQRS_University_System.Application.Features.Students.Commands.RemoveStudent;
using CQRS_University_System.Application.Features.Students.Queries.FilterStudents;
using CQRS_University_System.Application.Features.Students.Queries.GetAllStudents;
using CQRS_University_System.Application.Features.Students.Queries.GetStudentById;
using CQRS_University_System.Application.Features.Students.Queries.GetStudentCourses;
using CQRS_University_System.Domain.Commons;
using CQRS_University_System.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRS_University_System.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }



        /// <summary>
        /// Get a specific student by ID.
        /// </summary>
        /// <param name="Id">The student's ID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>The student details.</returns>
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] int Id, CancellationToken token)
        {
            var query = new GetStudentByIdQuery() { Id = Id };
            var result = await _mediator.Send(query, token);
            return Ok(result);
        }



        /// <summary>
        /// Get all students.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of all students.</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetStudents(CancellationToken token)
        {
            var query = new GetAllStudentsQuery();
            var result = await _mediator.Send(query, token);
            return Ok(result);
        }



        /// <summary>
        /// Get courses registered by a specific student.
        /// </summary>
        /// <param name="id">The student's ID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>List of courses.</returns>
        [HttpGet("{id}/courses")]
        public async Task<IActionResult> GetStudentCourses([FromRoute] int id, CancellationToken token)
        {
            var query = new GetStudentCoursesQuery() { Id = id };
            var result = await _mediator.Send(query, token);
            return Ok(result);
        }



        /// <summary>
        /// Filter students based on search, sort, and pagination options.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <param name="search">Search keyword (e.g., name).</param>
        /// <param name="orderBy">Sort field (e.g., CGPA, Name).</param>
        /// <param name="isDesc">Sort descending if true.</param>
        /// <param name="PageNo">Page number (1-based).</param>
        /// <param name="pageSize">Number of items per page.</param>
        /// <returns>Paged list of students matching filter criteria.</returns>
        [HttpGet("filter")]
        public async Task<IActionResult> FilterStudents(
            CancellationToken token,
            [FromQuery] string? search,
            [FromQuery] string? orderBy,
            [FromQuery] bool? isDesc,
            [FromQuery] int PageNo = 1,
            [FromQuery] int pageSize = 10)
        {
            var filter = new QueryFilterModel<Student>
            {
                isDesc = isDesc,
                skip = pageSize * (PageNo - 1),
                take = pageSize,
                orderBy = orderBy switch
                {
                    "CGPA" => s => s.CGPA,
                    _ => s => s.Name
                }
            };

            if (search is not null)
                filter.AddSearch(x => x.Name.Contains(search));

            var query = new FilterStudentsQuery() { filterModel = filter };
            var result = await _mediator.Send(query, token);
            return Ok(result);
        }



        /// <summary>
        /// Create a new student.
        /// </summary>
        /// <param name="dTO">Student creation data.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>The created student info.</returns>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] CreateStudentDTO dTO, CancellationToken token)
        {
            var command = new CreateStudentCommand() { dto = dTO };
            var result = await _mediator.Send(command, token);
            return Ok(result);
        }



        /// <summary>
        /// Delete a student by ID.
        /// </summary>
        /// <param name="Id">The student's ID.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>Status message.</returns>
        [HttpDelete("delete/{Id}")]
        public async Task<IActionResult> Delete([FromRoute] int Id, CancellationToken token)
        {
            var command = new RemoveStudentCommand() { Id = Id };
            var result = await _mediator.Send(command, token);
            return result
                ? Ok($"Successfully deleted Student with ID: {Id}")
                : throw new ArgumentException($"No student found with ID: {Id}");
        }
    }
}
