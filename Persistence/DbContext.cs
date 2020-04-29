using Domain;
using MicroOrm.Dapper.Repositories;
using System.Data;
using System.Data.SqlClient;

namespace Persistence
{
  public interface IDbContext
  {
    DapperRepository<DaftPhk3> DaftPhk3 { get; }
    IDbConnection Connection { get; }
  }

  public class DbContext : IDbContext
  {
    public DbContext(string connectionString)
    {
      Connection = new SqlConnection(connectionString);
      Connection.Open();
    }

    public IDbConnection Connection { get; }

    public DapperRepository<DaftPhk3> DaftPhk3 => new DapperRepository<DaftPhk3>(Connection);

  }
}