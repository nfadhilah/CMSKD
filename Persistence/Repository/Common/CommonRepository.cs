using Dapper;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Persistence.Repository.Common
{
  public class CommonRepository<T> : DapperRepository<T> where T : class
  {
    public CommonRepository(IDbConnection connection) : base(connection) { }

    public CommonRepository(
      IDbConnection connection, ISqlGenerator<T> sqlGenerator) : base(
      connection, sqlGenerator)
    { }

    private string GetTableName()
    {
      var att = typeof(T).GetCustomAttributes(
        typeof(TableAttribute), true
      ).FirstOrDefault();

      if (att == null)
        throw new Exception($"Table attribute not found in class {nameof(T)}");

      return (att as TableAttribute)?.Name;
    }

    private static string GetPropertyName(
      Expression<Func<T, object>> key)
    {
      var lambda =
        (LambdaExpression)key;
      MemberExpression memberExpression;

      if (lambda.Body is UnaryExpression expression)
      {
        var unaryExpression = expression;
        memberExpression =
          (MemberExpression)(unaryExpression.Operand);
      }
      else
      {
        memberExpression =
          (MemberExpression)(lambda.Body);
      }

      return ((PropertyInfo)memberExpression.Member).Name;
    }

    protected SqlBuilder.Template PaginatedQueryBuilder(
      SqlBuilder.Template baseQuery, uint offset, uint limit,
      List<string> orderBy)
    {
      var builder = new SqlBuilder();

      var query = builder.AddTemplate(limit > 0
        ? $@"{baseQuery.RawSql} /**orderby**/ OFFSET {offset} ROWS FETCH NEXT {limit} ROWS ONLY"
        : $@"{baseQuery.RawSql} /**orderby**/");

      foreach (var orderKey in orderBy)
      {
        builder.OrderBy($"{orderKey}");
      }

      return query;
    }

    public async Task BulkDeleteAsync(
      Expression<Func<T, object>> key, IEnumerable<long> ids,
      IDbTransaction transaction = null)
    {
      var tableName = GetTableName();

      var keyName = GetPropertyName(key).ToUpper();

      await Connection.ExecuteAsync(
        $"DELETE FROM {tableName} WHERE {keyName} IN @Ids",
        new { Ids = ids }, transaction);
    }
  }
}