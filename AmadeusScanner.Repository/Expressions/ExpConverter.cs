using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AmadeusScanner.Repository.Expressions
{
    internal class ExpConverter<T, TEntity> : ExpressionVisitor where TEntity : class
    {
        public Expression Convert(Expression expr)
        {
            return Visit(expr);
        }

        private ParameterExpression replaceParam;

        protected override Expression VisitLambda<Td>(Expression<Td> node)
        {
            if (typeof(Td) == typeof(Func<T, bool>))
            {
                replaceParam = Expression.Parameter(typeof(TEntity), "p");
                return Expression.Lambda<Func<TEntity, bool>>(Visit(node.Body), replaceParam);
            }
            return base.VisitLambda<Td>(node);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (node.Type == typeof(T))
                return replaceParam; // Expression.Parameter(typeof(DataObject), "p");
            return base.VisitParameter(node);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Member.DeclaringType == typeof(T))
            {
                var member = typeof(TEntity).GetMember(node.Member.Name, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).FirstOrDefault();
                if (member == null)
                    throw new InvalidOperationException("Cannot identify corresponding member of DataObject");
                return Expression.MakeMemberAccess(Visit(node.Expression), member);
            }
            return base.VisitMember(node);
        }
    }
}
