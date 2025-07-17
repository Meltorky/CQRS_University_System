using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRS_University_System.Application.Interfaces;
using CQRS_University_System.Infrastructure.Data;

namespace CQRS_University_System.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IStudentRepository Students { get; private set; }
        public ICourseRepository Courses { get; private set; }
        public IDepartmentRepository Departments { get; private set; }


        public UnitOfWork(AppDbContext context,
            IStudentRepository students,
            ICourseRepository courses,
            IDepartmentRepository departments)
        {
            _context = context;
            Students = students;
            Courses = courses;
            Departments = departments;
        }


        public async Task<int> SaveChangesAsync(CancellationToken token)
        {
            return await _context.SaveChangesAsync(token);
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
