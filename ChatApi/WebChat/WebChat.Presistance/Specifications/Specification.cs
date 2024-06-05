
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;


namespace WebChat.Presistance.Specifications
{
    public class Specification<TEntity> where TEntity : class
    {
        protected Specification(Expression<Func<TEntity, bool>>? criteria)=>Criteria = criteria;

        public Expression<Func<TEntity, bool>>? Criteria { get; }
        public List<Expression<Func<TEntity, object>>> IncludeExpression { get; } = new();
        public Expression<Func<TEntity, object>>? OrderByExpression { get; private set; }
        public Expression<Func<TEntity, object>>? OrderByDescendingExpression { get; private set; }
        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression) =>
            IncludeExpression.Add(includeExpression);
        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression) =>
           OrderByExpression = orderByExpression;
        protected void AddOrderDescendingBy(Expression<Func<TEntity, object>> orderByDescendingExpression) =>
           OrderByDescendingExpression = orderByDescendingExpression;


    }
}
