using CQRS_University_System.Application.Features.Departments.Commands.CreateDepartment;
using CQRS_University_System.Application.Features.Departments.Queries;
using CQRS_University_System.Application.Features.Departments.Queries.GetAllDepartments;
using CQRS_University_System.Application.Features.Departments.Queries.GetDepartmentCourses;
using CQRS_University_System.Application.Features.Departments.Queries.GetDepartmentStudents;
using MediatR;
using Microsoft.AspNetCore.Http;
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


        [HttpGet]
        public async Task<IActionResult> GetAllDepartments(CancellationToken cancellationToken)
        {
            var query = new GetAllDepartmentsQuery();
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet("courses")]
        public async Task<IActionResult> GetAllDepartmentCourses(CancellationToken token)
        {
            var query = new GetDepartmentCoursesQuery();
            var result = await _mediator.Send(query, token);
            return Ok(result);
        }

        [HttpGet("students")]
        public async Task<IActionResult> GetAllDepartmentStudents(CancellationToken token)
        {
            var query = new GetDepartmentStudentsQuery();
            var result = await _mediator.Send(query, token);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateDepartment([FromQuery] string name , CancellationToken token) 
        {
            var comand = new CreateDepartmentCommand { Name = name };
            var result = await _mediator.Send(comand , token);
            return Ok(result);
        }
    }
}
