using Persistence.Repository;
using System.Data;
using System.Data.SqlClient;

namespace Persistence
{
  public interface IDbContext
  {
    IDbConnection Connection { get; }
    DaftPhk3Repository DaftPhk3 { get; }
    DaftUnitRepository DaftUnit { get; }
    WebUserRepository WebUser { get; }
    RolesRepository Roles { get; }
    TahunRepository Tahun { get; }
    AppUserRepository AppUser { get; }
    PermissionRepository Permission { get; }
    MPgrmRepository MPgrm { get; }
    MKegiatanRepository MKegiatan { get; }
    PgrmUnitRepository PgrmUnit { get; }
    KegUnitRepository KegUnit { get; }
  }

  public class DbContext : IDbContext
  {
    public DbContext(string connectionString)
    {
      Connection = new SqlConnection(connectionString);
      Connection.Open();
    }

    public IDbConnection Connection { get; }
    public DaftUnitRepository DaftUnit => new DaftUnitRepository(Connection);
    public DaftPhk3Repository DaftPhk3 => new DaftPhk3Repository(Connection);
    public WebUserRepository WebUser => new WebUserRepository(Connection);
    public RolesRepository Roles => new RolesRepository(Connection);
    public TahunRepository Tahun => new TahunRepository(Connection);
    public AppUserRepository AppUser => new AppUserRepository(Connection);
    public PermissionRepository Permission => new PermissionRepository(Connection);
    public MPgrmRepository MPgrm => new MPgrmRepository(Connection);
    public MKegiatanRepository MKegiatan => new MKegiatanRepository(Connection);
    public PgrmUnitRepository PgrmUnit => new PgrmUnitRepository(Connection);
    public KegUnitRepository KegUnit => new KegUnitRepository(Connection);
  }
}