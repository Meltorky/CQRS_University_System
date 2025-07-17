using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace CQRS_University_System.Domain.Commons
{
    public class QueryFilterModel<T> where T : class
    {
        public List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> IncludeExpressions { get; } = new();
        public List<Expression<Func<T, bool>>> search { get; } = new();
        public int take { get; set; } = 10;
        public int? skip { get; set; }
        public Expression<Func<T, object>>? orderBy { get; set; }
        public bool? isDesc { get; set; }

        public void AddInclude(Func<IQueryable<T>, IIncludableQueryable<T, object>> include)
            => IncludeExpressions.Add(include);
        public void AddSearch(Expression<Func<T, bool>> criteria)
            => search.Add(criteria);
    }
}
