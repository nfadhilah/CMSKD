using Persistence.Repository;
using System.Data;
using System.Data.SqlClient;

namespace Persistence
{
  public interface IDbContext
  {
    DaftPhk3Repository DaftPhk3 { get; }
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

    public DaftPhk3Repository DaftPhk3 => new DaftPhk3Repository(Connection);
  }
}