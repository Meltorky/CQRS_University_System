using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRS_University_System.Domain.Entities;

namespace CQRS_University_System.Application.Interfaces
{
    public interface ICourseRepository : IBaseRepository<Course>
    {
    }
}
