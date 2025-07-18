using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRS_University_System.Application.Interfaces;
using CQRS_University_System.Domain.Commons;
using CQRS_University_System.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace CQRS_University_System.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }



        public async Task<T?> GetById(int Id,
            CancellationToken token,
            QueryFilterModel<T>? filterModel)
        {
            IQueryable<T> query = _context.Set<T>().AsQueryable();

            if (filterModel is not null && filterModel.IncludeExpressions is not null)
                foreach (var include in filterModel.IncludeExpressions)
                    query = include(query);

            return await query.SingleOrDefaultAsync(e => EF.Property<int>(e, "Id") == Id, token);
        }



        public async Task<List<T>> Filter(CancellationToken token, QueryFilterModel<T> filterModel)
        {
            IQueryable<T> query = _context.Set<T>().AsQueryable();

            if (filterModel.search.Any())
                foreach (var filter in filterModel.search)
                    query = query.Where(filter);

            if (filterModel.skip.HasValue)
                query = query.Skip(filterModel.skip.Value)
                    .Take(filterModel.take);

            if (filterModel.orderBy is not null)
            {
                query = filterModel.isDesc == false ?
                    query.OrderBy(filterModel.orderBy) :
                    query.OrderByDescending(filterModel.orderBy);
            }

            if (filterModel.IncludeExpressions.Count > 0)
                foreach (var include in filterModel.IncludeExpressions)
                    query = include(query);

            return await query.ToListAsync(token);
        }



        public async Task<List<T>> GetAll(CancellationToken token)
        {
            return await _context.Set<T>()
                .AsNoTracking()
                .ToListAsync(token);
        }



        public async Task<T> Add(T entity, CancellationToken token)
        {
            var result = await _context.Set<T>().AddAsync(entity, token);
            await _context.SaveChangesAsync(token);
            return result.Entity;
        }



        public async Task<bool> Update(T entity, CancellationToken token)
        {
            _context.Set<T>().Update(entity);
            return await _context.SaveChangesAsync(token) > 0;
        }



        public async Task<bool> Delete(T entity, CancellationToken token)
        {
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync(token) > 0;
        }

    }
}
