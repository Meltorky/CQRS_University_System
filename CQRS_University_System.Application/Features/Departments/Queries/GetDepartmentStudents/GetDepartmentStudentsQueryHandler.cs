using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRS_University_System.Application.Abstractions.CQRS;
using CQRS_University_System.Application.DTOs.Departments;
using CQRS_University_System.Application.DTOs.Students;
using CQRS_University_System.Application.Interfaces;
using CQRS_University_System.Application.Mappers;
using CQRS_University_System.Domain.Commons;
using CQRS_University_System.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CQRS_University_System.Application.Features.Departments.Queries.GetDepartmentStudents
{
    public class GetDepartmentStudentsQueryHandler : IQueryHandler<GetDepartmentStudentsQuery, List<DepartmentStudentsDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetDepartmentStudentsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<DepartmentStudentsDTO>> Handle(GetDepartmentStudentsQuery request, CancellationToken cancellationToken)
        {
            var filter = new QueryFilterModel<Department>();
            filter.AddInclude(x => x.Include(z => z.Students));

            var result = await _unitOfWork.Departments.Filter(cancellationToken, filter);

            return result.Select(d => new DepartmentStudentsDTO 
            {
                Id = d.Id,
                Name = d.Name,
                Students = d.Students.ToList().ToStudentDTOs()
            }).ToList();
        }
    }
}
