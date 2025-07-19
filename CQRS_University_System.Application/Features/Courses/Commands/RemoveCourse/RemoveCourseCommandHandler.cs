using CQRS_University_System.Application.Abstractions.CQRS;
using CQRS_University_System.Application.Commons.Exceptions;
using CQRS_University_System.Application.Interfaces;

namespace CQRS_University_System.Application.Features.Courses.Commands.RemoveCourse
{
    public class RemoveCourseCommandHandler : ICommandHandler<RemoveCourseCommand,bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RemoveCourseCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(RemoveCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _unitOfWork.Courses.GetById(request.Id, cancellationToken);

            if (course is null)
                throw new NotFoundException($"Course with Id {request.Id} not Exist !");

            return await _unitOfWork.Courses.Delete(course, cancellationToken);
        }
    }
}
