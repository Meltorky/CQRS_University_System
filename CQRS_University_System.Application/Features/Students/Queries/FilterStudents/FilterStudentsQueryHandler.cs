using CQRS_University_System.Application.Abstractions.CQRS;
using CQRS_University_System.Application.DTOs.Students;
using CQRS_University_System.Application.Interfaces;
using CQRS_University_System.Application.Mappers;

namespace CQRS_University_System.Application.Features.Students.Queries.FilterStudents
{
    public class FilterStudentsQueryHandler : IQueryHandler<FilterStudentsQuery, List<StudentDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public FilterStudentsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<StudentDTO>> Handle(FilterStudentsQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Students.Filter(cancellationToken,request.filterModel);
            
            return result.ToStudentDTOs();
        }
    }
}
