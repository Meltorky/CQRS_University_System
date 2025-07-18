using CQRS_University_System.Application.Abstractions.CQRS;
using CQRS_University_System.Application.DTOs.Departments;
using CQRS_University_System.Application.Interfaces;

namespace CQRS_University_System.Application.Features.Departments.Queries.GetAllDepartments
{
    public class GetAllDepartmentsQueryHandler : IQueryHandler<GetAllDepartmentsQuery, List<DepartmentDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllDepartmentsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<DepartmentDTO>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Departments.GetAll(cancellationToken);
            return result.Select(d => new DepartmentDTO 
            {
                Id = d.Id,
                Name = d.Name,
            }).ToList();
        }
    }
}
