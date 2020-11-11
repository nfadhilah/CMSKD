using MicroOrm.Dapper.Repositories.Config;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using System;
using System.Data;
using System.Data.SqlClient;
using Persistence.Repository.Auth;
using Persistence.Repository.DM;

namespace Persistence
{
  public class DbContext : IDbContext
  {
    private bool _disposed;


    public DbContext(string connectionString)
    {
      MicroOrmConfig.SqlProvider = SqlProvider.MSSQL;
      Connection = new SqlConnection(connectionString);
      Connection.Open();
    }

    public IDbConnection Connection { get; }
    public WebUserRepository WebUser => new WebUserRepository(Connection);
    public WebOtorRepository WebOtor => new WebOtorRepository(Connection);
    public TahunRepository Tahun => new TahunRepository(Connection);
    public PegawaiRepository Pegawai => new PegawaiRepository(Connection);
    public WebGroupRepository WebGroup => new WebGroupRepository(Connection);
    public WebRoleRepository WebRole => new WebRoleRepository(Connection);

    protected virtual void Dispose(bool disposing)
    {
      if (!_disposed)
      {
        if (disposing)
        {
          Connection?.Dispose();
        }
      }

      _disposed = true;
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    ~DbContext()
    {
      Dispose(false);
    }
  }
}