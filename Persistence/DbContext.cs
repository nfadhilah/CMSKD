using MicroOrm.Dapper.Repositories.Config;
using MicroOrm.Dapper.Repositories.SqlGenerator;
using Persistence.Repository;
using System.Data;
using System.Data.SqlClient;

namespace Persistence
{
  public interface IDbContext
  {
    IDbConnection Connection { get; }
    BendRepository Bend { get; }
    DaftPhk3Repository DaftPhk3 { get; }
    JBankRepository JBank { get; }
    JBendRepository JBend { get; }
    JDanaRepository JDana { get; }
    JBKasRepository JBKas { get; }
    JBMRepository JBM { get; }
    JTransRepository JTrans { get; }
    JAKasRepository JAKas { get; }
    JnsAkunRepository JnsAkun { get; }
    JSatuanRepository JSatuan { get; }
    JBayarRepository JBayar { get; }
    ProfilRepository Profil { get; }
    DaftFungsiRepository DaftFungsi { get; }
    DaftBankRepository DaftBank { get; }
    BkBKasRepository BkBKas { get; }
    PegawaiRepository Pegawai { get; }
    DaftRekeningRepository DaftRekening { get; }
    PajakRepository Pajak { get; }
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
    UrusanUnitRepository UrusanUnit { get; }
    GolonganRepository Golongan { get; }
    SifatKegRepository SifatKeg { get; }
    StruRekRepository StruRek { get; }
    StruUnitRepository StruUnit { get; }
    KPARepository KPA { get; }
    BendKPARepository BendKPA { get; }
  }

  public class DbContext : IDbContext
  {
    public DbContext(string connectionString)
    {
      MicroOrmConfig.SqlProvider = SqlProvider.MSSQL;
      Connection = new SqlConnection(connectionString);
      Connection.Open();
    }

    public IDbConnection Connection { get; }
    public DaftUnitRepository DaftUnit => new DaftUnitRepository(Connection);
    public BendRepository Bend => new BendRepository(Connection);
    public DaftPhk3Repository DaftPhk3 => new DaftPhk3Repository(Connection);
    public JBankRepository JBank => new JBankRepository(Connection);
    public JBendRepository JBend => new JBendRepository(Connection);
    public JDanaRepository JDana => new JDanaRepository(Connection);
    public JBKasRepository JBKas => new JBKasRepository(Connection);
    public JBMRepository JBM => new JBMRepository(Connection);
    public JTransRepository JTrans => new JTransRepository(Connection);
    public JAKasRepository JAKas => new JAKasRepository(Connection);
    public JnsAkunRepository JnsAkun => new JnsAkunRepository(Connection);
    public JSatuanRepository JSatuan => new JSatuanRepository(Connection);
    public JBayarRepository JBayar => new JBayarRepository(Connection);
    public ProfilRepository Profil => new ProfilRepository(Connection);
    public DaftBankRepository DaftBank => new DaftBankRepository(Connection);
    public DaftFungsiRepository DaftFungsi => new DaftFungsiRepository(Connection);
    public BkBKasRepository BkBKas => new BkBKasRepository(Connection);
    public PegawaiRepository Pegawai => new PegawaiRepository(Connection);
    public DaftRekeningRepository DaftRekening => new DaftRekeningRepository(Connection);
    public PajakRepository Pajak => new PajakRepository(Connection);
    public WebUserRepository WebUser => new WebUserRepository(Connection);
    public RolesRepository Roles => new RolesRepository(Connection);
    public TahunRepository Tahun => new TahunRepository(Connection);
    public AppUserRepository AppUser => new AppUserRepository(Connection);
    public PermissionRepository Permission => new PermissionRepository(Connection);
    public MPgrmRepository MPgrm => new MPgrmRepository(Connection);
    public MKegiatanRepository MKegiatan => new MKegiatanRepository(Connection);
    public PgrmUnitRepository PgrmUnit => new PgrmUnitRepository(Connection);
    public KegUnitRepository KegUnit => new KegUnitRepository(Connection);
    public UrusanUnitRepository UrusanUnit => new UrusanUnitRepository(Connection);
    public GolonganRepository Golongan => new GolonganRepository(Connection);
    public SifatKegRepository SifatKeg => new SifatKegRepository(Connection);
    public StruRekRepository StruRek => new StruRekRepository(Connection);
    public StruUnitRepository StruUnit => new StruUnitRepository(Connection);
    public KPARepository KPA => new KPARepository(Connection);
    public BendKPARepository BendKPA => new BendKPARepository(Connection);
  }
}