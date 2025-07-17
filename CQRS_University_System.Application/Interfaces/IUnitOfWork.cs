using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS_University_System.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IStudentRepository Students { get; }
        ICourseRepository Courses { get; }
        IDepartmentRepository Departments { get; }
        Task<int> SaveChangesAsync(CancellationToken token);
    }
}
