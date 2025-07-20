using CQRS_University_System.Application.DTOs.Courses;
using CQRS_University_System.Application.Features.Courses.Commands.CreateCourse;
using CQRS_University_System.Application.Features.Courses.Commands.RemoveCourse;
using CQRS_University_System.Application.Features.Courses.Queries.GetCourseById;
using CQRS_University_System.Application.Features.Courses.Queries.GetCourseStudents;
using MediatR;
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
        /// Sasso manga
        /// </summary>
        /// <param name="Id">id of course</param>
        /// <param name="token"></param>
        /// <response code="200">Returns the choosen Course</response>
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCourseById([FromRoute] int Id, CancellationToken token)
        {
            var query = new GetCourseByIdQuery() { Id = Id};
            var result = await _mediator.Send(query,token);
            return Ok(result);

        }



        [HttpGet("{Id}/students")]
        public async Task<IActionResult> GetCourseStudents([FromRoute] int Id, CancellationToken token)
        {
            var query = new GetCourseStudentsQuery() { Id = Id };
            var result = await _mediator.Send(query, token);
            return Ok(result);
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] CreateCourseDTO dto, CancellationToken token)
        {
            var command = new CreateCourseCommand() { dto = dto};
            var result = await _mediator.Send(command, token);
            return Ok(result);
        }


        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromRoute] int Id, CancellationToken token)
        {
            var command = new RemoveCourseCommand() { Id = Id };
            var result = await _mediator.Send(command, token);
            return result ?
                Ok($"Successfully delete Course with ID: {Id}") :
                throw new ArgumentException();
        }
    }
}
