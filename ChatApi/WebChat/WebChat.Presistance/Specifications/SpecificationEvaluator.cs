using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebChat.Presistance.Specifications
{
    public class SpecificationEvaluator
    {
        public static IQueryable<TEntity> GetQuery<TEntity>(IQueryable<TEntity> inputQuery,Specification<TEntity> specification)
            where TEntity : class
        {
            IQueryable<TEntity> queryable = inputQuery;
            if(specification.Criteria!= null)
                queryable = queryable.Where(specification.Criteria);
         queryable=specification.IncludeExpression.Aggregate(queryable, (current, includeExpression) => current.Include(includeExpression));
            if(specification.OrderByExpression!=null)
                queryable=queryable.OrderBy(specification.OrderByExpression);
            else if(specification.OrderByDescendingExpression is not null)
                queryable = queryable.OrderByDescending(specification.OrderByDescendingExpression);
            return queryable;

        }

    }
}
