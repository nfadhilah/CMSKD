﻿using MicroOrm.Dapper.Repositories.Config;
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
  }
}