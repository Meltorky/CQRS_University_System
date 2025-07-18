using CQRS_University_System.Application.Abstractions.CQRS;
using CQRS_University_System.Application.DTOs.Departments;

namespace CQRS_University_System.Application.Features.Departments.Queries.GetDepartmentStudents
{
    public class GetDepartmentStudentsQuery : IQuery<List<DepartmentStudentsDTO>>
    {
    }
}
