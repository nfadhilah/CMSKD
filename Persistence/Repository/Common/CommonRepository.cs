using Dapper;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
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

      if (att == null) throw new Exception($"Table attribute not found in class {nameof(T)}");

      return (att as TableAttribute)?.Name;
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
      string keyColumn, IEnumerable<long> ids,
      IDbTransaction transaction = null)
    {
      var tableName = GetTableName();

      await Connection.ExecuteAsync(
        $"DELETE FROM {tableName} WHERE {keyColumn} IN @Ids",
        new { Ids = ids }, transaction);
    }
  }
}