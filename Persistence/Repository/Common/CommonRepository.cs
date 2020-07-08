using Dapper;
using MicroOrm.Dapper.Repositories;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System.Collections.Generic;
using System.Data;

namespace Persistence.Repository.Common
{
  public class CommonRepository<T> : DapperRepository<T> where T : class
  {
    public CommonRepository(IDbConnection connection) : base(connection) { }

    public CommonRepository(
      IDbConnection connection, ISqlGenerator<T> sqlGenerator) : base(
      connection, sqlGenerator)
    { }

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
  }
}