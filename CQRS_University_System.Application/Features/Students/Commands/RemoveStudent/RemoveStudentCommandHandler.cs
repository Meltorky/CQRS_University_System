using System;
using System.Collections.Generic;
using CQRS_University_System.Application.Abstractions.CQRS;
using CQRS_University_System.Application.Commons.Exceptions;
using CQRS_University_System.Application.Interfaces;

namespace CQRS_University_System.Application.Features.Students.Commands.RemoveStudent
{
    public class RemoveStudentCommandHandler : ICommandHandler<RemoveStudentCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RemoveStudentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(RemoveStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _unitOfWork.Students.GetById(request.Id,cancellationToken);

            if (student is null)
                throw new NotFoundException($"Student with Id {request.Id} not Exist !");

            return await _unitOfWork.Students.Delete(student,cancellationToken);

        }
    }
}
