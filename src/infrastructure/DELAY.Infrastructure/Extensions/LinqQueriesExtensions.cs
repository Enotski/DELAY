using DELAY.Infrastructure.Builders;
using System.Linq.Expressions;

namespace DELAY.Infrastructure.Extensions
{
    public static class LinqQueries
    {
        #region Recording Time Extensions
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="getDate"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
		public static Expression<Func<T, bool>> WhereDateBetween<T>(this Expression<Func<T, bool>> expression, Expression<Func<T, DateTime>> getDate, string fromDate, string toDate)
        {
            if (string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate))
                return expression;  // The simplest query is no query

            DateTime tmp;
            DateTime? tmpTo = null;
            if (!string.IsNullOrEmpty(toDate) && DateTime.TryParse(toDate, out tmp))
            {
                tmpTo = tmp.AddDays(1).ToUniversalTime();
            }

            DateTime? tmpFrom = null;
            if (!string.IsNullOrEmpty(fromDate) && DateTime.TryParse(fromDate, out tmp))
            {
                tmpFrom = tmp.ToUniversalTime();
            }
            return expression.And(getDate.Chain(DateBetween(tmpFrom, tmpTo)));
        }
        /// <summary>
        /// Выражение для выбора записей в промежутке
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        private static Expression<Func<DateTime, bool>> DateBetween(DateTime? fromDate, DateTime? toDate)
        {
            if (!toDate.HasValue && !fromDate.HasValue)
            {
                return date => true;
            }

            if (toDate == null)
            {
                return date => fromDate <= date;
            }

            if (fromDate == null)
            {
                return date => toDate >= date;
            }

            return date => fromDate <= date && toDate >= date;
        }
        private static Expression<Func<TIn, TOut>> Chain<TIn, TInterstitial, TOut>(this Expression<Func<TIn, TInterstitial>> inner, Expression<Func<TInterstitial, TOut>> outer)
        {
            var visitor = new SwapVisitor(outer.Parameters[0], inner.Body);
            return Expression.Lambda<Func<TIn, TOut>>(visitor.Visit(outer.Body), inner.Parameters);
        }
        private class SwapVisitor : ExpressionVisitor
        {
            private readonly Expression _source, _replacement;

            public SwapVisitor(Expression source, Expression replacement)
            {
                _source = source;
                _replacement = replacement;
            }

            public override Expression Visit(Expression node)
            {
                return node == _source ? _replacement : base.Visit(node);
            }
        }
        #endregion
    }
}
