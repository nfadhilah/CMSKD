using MicroOrm.Dapper.Repositories.Config;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository.Auth;
using Persistence.Repository.DM;
using Persistence.Repository.TU;
using System;
using System.Data;
using System.Data.SqlClient;

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
    public SP2DRepository SP2D => new SP2DRepository(Connection);
    public SP2DDetRRepository Sp2DDetR => new SP2DDetRRepository(Connection);
    public SP2DDetRTLRepository SP2DDetRTL => new SP2DDetRTLRepository(Connection);
    public SP2DPjkRepository SP2DPjk => new SP2DPjkRepository(Connection);
    public SP2DDetBRepository SP2DDetB => new SP2DDetBRepository(Connection);
    public SP2DDetDRepository SP2DDetD => new SP2DDetDRepository(Connection);
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