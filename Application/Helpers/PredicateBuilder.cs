using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Application.Helpers
{
  public static class PredicateBuilder
  {
    public static Expression<Func<T, bool>> True<T>()
    {
      return param => true;
    }

    public static Expression<Func<T, bool>> False<T>()
    {
      return param => false;
    }

    public static Expression<Func<T, bool>> Create<T>(
      Expression<Func<T, bool>> predicate)
    {
      return predicate;
    }

    public static Expression<Func<T, bool>> And<T>(
      this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
    {
      return first.Compose(second, Expression.AndAlso);
    }

    public static Expression<Func<T, bool>> ComposeWithAnd<T>(
      List<Expression<Func<T, bool>>> collections)
    {
      if (!collections.Any()) return null;

      var predicate = True<T>();

      for (var i = 0; i < collections.Count; i++)
      {
        predicate = i == 0 ? collections[i] : predicate.And(collections[i]);
      }

      return predicate;
    }

    public static Expression<Func<T, bool>> ComposeWithOr<T>(
      List<Expression<Func<T, bool>>> collections)
    {
      if (!collections.Any()) return null;

      var predicate = True<T>();

      for (var i = 0; i < collections.Count; i++)
      {
        predicate = i == 0 ? collections[i] : predicate.Or(collections[i]);
      }

      return predicate;
    }

    public static Expression<Func<T, bool>> Or<T>(
      this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
    {
      return first.Compose(second, Expression.OrElse);
    }

    public static Expression<Func<T, bool>> Not<T>(
      this Expression<Func<T, bool>> expression)
    {
      var negated = Expression.Not(expression.Body);
      return Expression.Lambda<Func<T, bool>>(negated, expression.Parameters);
    }

    static Expression<T> Compose<T>(
      this Expression<T> first, Expression<T> second,
      Func<Expression, Expression, Expression> merge)
    {
      var map = first.Parameters
        .Select((f, i) => new { f, s = second.Parameters[i] })
        .ToDictionary(p => p.s, p => p.f);

      var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

      return Expression.Lambda<T>(merge(first.Body, secondBody),
        first.Parameters);
    }

    class ParameterRebinder : ExpressionVisitor
    {
      readonly Dictionary<ParameterExpression, ParameterExpression> map;

      ParameterRebinder(
        Dictionary<ParameterExpression, ParameterExpression> map)
      {
        this.map =
          map ?? new Dictionary<ParameterExpression, ParameterExpression>();
      }

      public static Expression ReplaceParameters(
        Dictionary<ParameterExpression, ParameterExpression> map,
        Expression exp)
      {
        return new ParameterRebinder(map).Visit(exp);
      }

      protected override Expression VisitParameter(ParameterExpression p)
      {
        if (map.TryGetValue(p, out var replacement))
        {
          p = replacement;
        }

        return base.VisitParameter(p);
      }
    }
  }
}