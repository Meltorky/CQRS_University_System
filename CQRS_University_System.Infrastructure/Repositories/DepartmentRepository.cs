using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRS_University_System.Application.Interfaces;
using CQRS_University_System.Domain.Entities;
using CQRS_University_System.Infrastructure.Data;

namespace CQRS_University_System.Infrastructure.Repositories
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        private readonly AppDbContext _context;
        public DepartmentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
