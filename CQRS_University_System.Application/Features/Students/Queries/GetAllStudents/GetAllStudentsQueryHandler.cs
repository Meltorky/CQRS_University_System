using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRS_University_System.Application.Abstractions.CQRS;
using CQRS_University_System.Application.DTOs.Students;
using CQRS_University_System.Application.Interfaces;
using CQRS_University_System.Application.Mappers;
using CQRS_University_System.Domain.Commons;
using CQRS_University_System.Domain.Entities;

namespace CQRS_University_System.Application.Features.Students.Queries.GetAllStudents
{
    public class GetAllStudentsQueryHandler : IQueryHandler<GetAllStudentsQuery,List<StudentDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllStudentsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<StudentDTO>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Students.GetAll(cancellationToken);

            return result.ToStudentDTOs();
        }
    }
}
