using CQRS_University_System.Application.Features.Students.Queries.FilterStudents;
using CQRS_University_System.Application.Features.Students.Queries.GetAllStudents;
using CQRS_University_System.Application.Features.Students.Queries.GetStudentCourses;
using CQRS_University_System.Domain.Commons;
using CQRS_University_System.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("all")]
        public async Task<IActionResult> GetStudents( CancellationToken token)
        {
            var query = new GetAllStudentsQuery();
            var result = await _mediator.Send(query, token);
            return Ok(result);
        }


        [HttpGet("{id}/courses")]
        public async Task<IActionResult> GetStudentCourses(int id , CancellationToken token) 
        {
            var query = new GetStudentCoursesQuery() { Id = id };
            var result = await _mediator.Send(query, token);
            return Ok(result);
        }



        [HttpGet("filter")]
        public async Task<IActionResult> FilterStudents(
            CancellationToken token,
            [FromQuery] string? search,
            [FromQuery] string? orderBy,
            [FromQuery] bool? isDesc,
            [FromQuery] int PageNo = 1,
            [FromQuery] int pageSize = 10
            )
        {
            var filter = new QueryFilterModel<Student>();
            filter.isDesc = isDesc;
            filter.skip = pageSize * (PageNo - 1);
            filter.take = pageSize;

            filter.orderBy = orderBy switch 
            {
                "CGPA" => s => s.CGPA,
                _=> s => s.Name
            };

            if (search is not null) 
                filter.AddSearch(x => x.Name.Contains(search));

            var query = new FilterStudentsQuery() {filterModel = filter };
            var result = await _mediator.Send(query, token);
            return Ok(result);
        }


    }
}
